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
        public AddEditEntryDialog()
        {
            InitializeComponent();
            instanceC = CryptoInstance.GetInstance();
        }
        public AddEditEntryDialog(Entry entry)
        {
            InitializeComponent();
            instanceC = CryptoInstance.GetInstance();
            textBoxName.Text = entry.Name;
            textBoxUsername.Text = entry.Username;
            textBoxPassword.Text = instanceC.decryptString(Text);
            textBoxURL.Text = entry.URL;
            textBoxNote.Text = entry.Note;
        }

        public Entry? getEntry()
        {
            if(textBoxPassword.Text == string.Empty)
            {
                return null;
            }
            Entry current = new Entry { };
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
