using Database.PasswordDB;
using PasswordManager.Crypto;
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
    public partial class AddEditEntryDialog : Form
    {
        private CryptoInstance instanceC;
        private List<EntryGroup> groups;
        private Entry? exist;
        public AddEditEntryDialog(List<EntryGroup> groups)
        {
            InitializeComponent();
            instanceC = CryptoInstance.GetInstance();
            this.groups= groups;
            comboBoxGroup.Items.Add("None");
            foreach(var group in groups)
            {
                comboBoxGroup.Items.Add(group.Name);
            }
        }
        public AddEditEntryDialog(List<EntryGroup> groups,Entry entry) : this(groups)
        {
            exist = entry;
            textBoxName.Text = entry.Name;
            textBoxUsername.Text = entry.Username;
            //textBoxPassword.Text = instanceC.decryptString(Text);
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
            
            if(textBoxPassword.Text == string.Empty || DialogResult == DialogResult.Cancel)
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
            if((comboBoxGroup.SelectedIndex-1) >= 0)
            {
                current.GroupId = groups[comboBoxGroup.SelectedIndex - 1].Id;
            }
            else
            {
                current.GroupId = null;
            }
            return current;
        }
        private bool valid()
        {
            //TODO: Implementalni
            return true;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!valid())
                DialogResult = DialogResult.None;
        }
    }
}
