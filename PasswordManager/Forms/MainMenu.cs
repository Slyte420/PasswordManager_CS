using Database.PasswordDB;
using PasswordManager.Crypto;
using System.Data;

namespace PasswordManager.Forms
{
    public partial class MainMenu : Form
    {
        private SQLSContext context = null!;
        private User user = null!;
        private List<Entry> entries = null!;
        private List<EntryGroup> groups = null!;
        private BindingSource bs = null!;
        private FirstMenu firstmenu = null!;
        int[] columnsDisabled = { 0, 4, 6, 7, 8, 9 };
        public MainMenu(User user, FirstMenu firstmenu)
        {
            this.user = user;
            this.firstmenu = firstmenu;
            InitializeComponent();


        }
        private void clear()
        {
            CryptoInstance instance = CryptoInstance.GetInstance();
            instance.reset();
            context.Dispose();
        }
        private async void MainMenu_Load(object sender, EventArgs e)
        {
            context = new SQLSContext();
            user = (from users in context.Users
                    where users.Username == user.Username
                    select users).First();
            var taskGroups = AsyncOperationsPassDB.getGroupsAsync(user.Id);
            var taskEntries = AsyncOperationsPassDB.getEntriesAsync(user.Id);
            groups = await taskGroups;

            bs = new BindingSource();

            comboBoxGroup.Items.Add("All");
            comboBoxGroup.SelectedIndex = 0;
            foreach (var gr in groups)
            {
                comboBoxGroup.Items.Add(gr.Name);
            }
            entries = await taskEntries;
            bs.DataSource = entries;
            dataGridViewData.DataSource = bs;
            foreach (int i in columnsDisabled)
            {
                dataGridViewData.Columns[i].Visible = false;
            }

        }
        private void refresh()
        {
            bs.ResetBindings(false);
        }
        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entries == null)
            {
                return;
            }
            int index = comboBoxGroup.SelectedIndex - 1;
            if (index >= 0)
            {
                int groupId = groups[index].Id;
                var query = from entry in entries
                            where entry.GroupId == groupId
                            select entry;
                bs.DataSource = query.ToList();
            }
            else
            {
                bs.DataSource = entries;

            }
            refresh();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Entry? entry = null;
            using (AddEditEntryDialog dialog = new AddEditEntryDialog(groups))
            {
                dialog.Text = "Add";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    entry = dialog.getEntry();
                }
            }
            if (entry == null)
            {
                return;
            }
            entry.UserId = user.Id;
            context.Add(entry);
            entries.Add(entry);
            context.SaveChanges();
            refresh();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if(dataGridViewData.SelectedRows.Count == 0)
            {
                return;
            }
            
            int index = dataGridViewData.SelectedRows[0].Index;
            List<Entry>? currentEntries = bs.DataSource as List<Entry>;

            if (currentEntries == null)
            {
                return;
            }
            Entry? currentEntry = currentEntries[index];
            if (currentEntry == null)
            {
                return;
            }
            Entry? result = context.Entries.SingleOrDefault(entry => entry.Id == currentEntry.Id);
            if (result == null)
            {
                return;
            }
            using (AddEditEntryDialog dialog = new AddEditEntryDialog(groups, result))
            {
                dialog.Text = "Edit";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    result = dialog.getEntry();
                }
            }
            if (result == null)
            {
                return;
            }

            context.Entries.Update(result);
            currentEntries[index] = result;
            index = entries.FindIndex(entry => entry.Id == currentEntry.Id);

            entries[index] = result;
            context.SaveChanges();
            refresh();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridViewData.SelectedRows[0].Index;
            List<Entry>? currentEntries = bs.DataSource as List<Entry>;

            if (currentEntries == null)
            {
                return;
            }
            Entry? currentEntry = currentEntries[index];
            if (currentEntry == null)
            {
                return;
            }
            Entry? result = context.Entries.SingleOrDefault(entry => entry.Id == currentEntry.Id);
            if (result == null)
            {
                return;
            }
            context.Entries.Remove(result);
            currentEntries.RemoveAt(index);
            index = entries.FindIndex(entry => entry.Id == currentEntry.Id);
            if (index >= 0)
            {
                entries.RemoveAt(index);
            }
            context.SaveChanges();
            refresh();
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            EntryGroup? groupE = null;
            using (AddGroupDialog dialog = new AddGroupDialog())
            {
                dialog.Text = "Add";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    groupE = dialog.getGroup();
                }
            }
            if (groupE == null)
            {
                return;
            }
            groupE.userId = user.Id;
            context.Add(groupE);
            comboBoxGroup.Items.Add(groupE.Name);
            groups.Add(groupE);
            context.SaveChanges();
        }

        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            int index = comboBoxGroup.SelectedIndex;
            int groupIndex = index - 1;
            if (groupIndex < 0)
            {
                return;
            }
            EntryGroup? groupE = context.EntryGroups.SingleOrDefault(group => group.Id == groups[groupIndex].Id);
            if (groupE == null)
            {
                return;
            }
            using (AddGroupDialog dialog = new AddGroupDialog(groupE))
            {
                dialog.Text = "Edit";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    groupE = dialog.getGroup();
                }
            }
            if (groupE == null)
            {
                return;
            }

            context.EntryGroups.Update(groupE);
            comboBoxGroup.Items.RemoveAt(index);
            comboBoxGroup.Items.Insert(index, groupE.Name);
            groups[groupIndex] = groupE;
            comboBoxGroup.SelectedIndex = index;
            context.SaveChanges();
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            int index = comboBoxGroup.SelectedIndex;
            if ((index - 1) < 0)
            {
                return;
            }
            EntryGroup? groupE = context.EntryGroups.SingleOrDefault(group => group.Id == groups[index - 1].Id);
            if (groupE == null)
            {
                return;
            }
            context.EntryGroups.Remove(groupE);
            comboBoxGroup.Items.RemoveAt(index);
            var query = from entries in context.Entries
                        where entries.GroupId == groupE.Id
                        select entries;
            var GEntries = context.Entries.Where(e => e.GroupId == groupE.Id);
            if (GEntries.Any())
            {
                foreach (Entry entry in GEntries)
                {
                    entry.GroupId = null;
                }
            }
            groups.RemoveAt(index - 1);
            comboBoxGroup.SelectedIndex = 0;
            context.SaveChanges();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            clear();
            firstmenu.Show();
        }

        private void buttonCopyClipBoard_Click(object sender, EventArgs e)
        {
            if (dataGridViewData.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridViewData.SelectedRows[0].Index;
            List<Entry>? currentList = bs.DataSource as List<Entry>;
            if (currentList is null)
            {
                return;
            }
            
            Entry selectedEntry = currentList[index];
            if (selectedEntry == null)
            {
                return;
            }
            CryptoInstance instance = CryptoInstance.GetInstance();
            Clipboard.SetText(instance.decryptString(selectedEntry.Password));
        }

        private void buttonShowPass_Click(object sender, EventArgs e)
        {
            if (dataGridViewData.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridViewData.SelectedRows[0].Index;
            List<Entry>? currentList = bs.DataSource as List<Entry>;
            if (currentList is null)
            {
                return;
            }

            Entry selectedEntry = currentList[index];
            if (selectedEntry == null)
            {
                return;
            }
            CryptoInstance instance = CryptoInstance.GetInstance();
            MessageBox.Show(instance.decryptString(selectedEntry.Password),"Password for selected entry");
        }

        private void buttonFindEntry_Click(object sender, EventArgs e)
        {
            using (FindEntryDialog dialog = new FindEntryDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string name = dialog.getName();
                    if(name == String.Empty)
                    {
                        return;
                    }
                    List<Entry>? currentEntries = bs.DataSource as List<Entry>;
                    if(currentEntries == null)
                    {
                        return;
                    }
                    var query = from entry in currentEntries
                                where entry.Name == name
                                select entry;
                    bs.DataSource = query.ToList();
                    refresh();
                }
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            clear();
            firstmenu.Close();
        }
    }
}
