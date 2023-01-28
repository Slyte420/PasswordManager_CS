using Database.PasswordDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager.Forms
{
    public partial class MainMenu : Form
    {
        private SQLSContext context = null!;
        private User? user;
        private List<Entry> entries= null!;
        private List<EntryGroup> groups = null!;
        private BindingSource bs = null!;
        int[] columnsDisabled = {0,6,7,8,9 };
        public MainMenu()
        {
            InitializeComponent();
            
            
        }
       
        private async void MainMenu_Load(object sender, EventArgs e)
        {
            context = new SQLSContext();
            user = (from users in context.Users
                    select users).First();
            var taskGroups = AsyncOperationsPassDB.getGroupsAsync(user.Id);
            var taskEntries = AsyncOperationsPassDB.getEntriesAsync(user.Id);
            groups = await taskGroups;
            
            bs = new BindingSource();
            
            comboBoxGroup.Items.Add("All");
            comboBoxGroup.SelectedIndex= 0;
            foreach(var gr in groups) {    
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
            if(entries == null)
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
                //foreach (int i in columnsDisabled)
                //{
                //    dataGridViewData.Columns[i].Visible = false;
                //}
            }
            refresh();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (user == null)
            {
                return;
            }
            Entry? entry= null;
            using (AddEditEntryDialog dialog = new AddEditEntryDialog(groups))
            {
                dialog.Text = "Add";
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    entry = dialog.getEntry();
                }
            }
            if(entry == null)
            {
                return;
            }
            entry.UserId = user.Id;
            context.Add(entry);
            entries.Add(entry);
            refresh();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int index = dataGridViewData.SelectedRows[0].Index;
            List<Entry>? currentEntries = bs.DataSource as List<Entry>;
            
            if (currentEntries == null)
            {
                return;
            }
            Entry? currentEntry = currentEntries[index];
            if(currentEntry == null)
            {
                return;
            }
            Entry? result = context.Entries.SingleOrDefault(entry=> entry.Id == currentEntry.Id);
            if (result == null)
            {
                return;
            }
            using (AddEditEntryDialog dialog = new AddEditEntryDialog(groups,result))
            {
                dialog.Text = "Add";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    result = dialog.getEntry();
                }
            }
            if(result == null)
            {
                return;
            }

            context.Entries.Update(result);
            currentEntries[index] = result;
            index = entries.FindIndex(entry => entry.Id == currentEntry.Id);
            
            entries[index] = result;
            //context.SaveChanges();
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
    }
}
