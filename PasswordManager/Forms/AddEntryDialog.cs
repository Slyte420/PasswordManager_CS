using Database.PasswordDB;
using PasswordManager.Crypto;
using System.Data;

namespace PasswordManager.Forms
{
    public partial class AddEditEntryDialog : Form
    {
        private CryptoInstance instanceC;
        private List<EntryGroup> groups;
        private Entry? exist;
        public AddEditEntryDialog(List<EntryGroup> groups)
        {
            InitializeComponent();
            instanceC = CryptoInstance.GetInstance();
            this.groups = groups;
            comboBoxGroup.Items.Add("None");
            foreach (var group in groups)
            {
                comboBoxGroup.Items.Add(group.Name);
            }
            comboBoxGroup.SelectedIndex= 0;
        }
        public AddEditEntryDialog(List<EntryGroup> groups, Entry entry) : this(groups)
        {
            exist = entry;
            textBoxName.Text = entry.Name;
            textBoxUsername.Text = entry.Username;
            textBoxPassword.Text = instanceC.decryptString(entry.Password);
            textBoxURL.Text = entry.URL;
            textBoxNote.Text = entry.Note;
            var query = (from gr in groups
                         where gr.Id == entry.GroupId
                         select gr);
            if (query.Any())
            {
                var result = query.First();
                comboBoxGroup.SelectedIndex = groups.IndexOf(result) + 1;
            }
        }

        public Entry? getEntry()
        {

            if (textBoxPassword.Text == string.Empty || DialogResult == DialogResult.Cancel)
            {
                return null;
            }
            Entry? current = exist;
            if (current == null)
            {
                current = new Entry() { Name = textBoxName.Text, Username = textBoxUsername.Text, Note = textBoxNote.Text, URL = textBoxURL.Text };
            }
            else
            {
                current.Name = textBoxName.Text;
                current.Username = textBoxUsername.Text;
                current.URL = textBoxURL.Text;
                current.Note = textBoxNote.Text;

            }
            current.Password = instanceC.encryptString(textBoxPassword.Text);
            if ((comboBoxGroup.SelectedIndex - 1) >= 0)
            {
                current.GroupId = groups[comboBoxGroup.SelectedIndex - 1].Id;
            }
            else
            {
                current.GroupId = null;
            }
            return current;
        }
        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text == String.Empty)
            {
                MessageBox.Show("Please include a name for entry!");
                DialogResult = DialogResult.None;
            }
            if (!RNGPassword.validPassword(textBoxPassword.Text))
            {
                MessageBox.Show("Not a valid password!");
                DialogResult = DialogResult.None;
            }
                
        }

        private void buttonGenPass_Click(object sender, EventArgs e)
        {
            using(GeneratePasswordDialog dialog = new GeneratePasswordDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxPassword.Text = dialog.generatePassword();
                }
            }
        }
    }
}
