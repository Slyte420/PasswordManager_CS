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
    public partial class LoginMenu : Form
    {
        private string? filePath;
        private CryptoInstance? instanceC;
        private MainMenu? mainMenu;
        private FirstMenu firstmenu;
        public LoginMenu(FirstMenu firstmenu)
        {
            this.firstmenu = firstmenu;
            InitializeComponent();
        }

        private void buttonGetKeyFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogKey.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialogKey.FileName;
            }
        }

        private void reset()
        {
            textBoxUsername.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            filePath = string.Empty;
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
                instanceC = CryptoInstance.GetInstance();
                if (filePath == null)
                {
                    MessageBox.Show("Key file not selected!");
                    return;
                }
                string username = textBoxUsername.Text;
                if (username.Length <= 0)
                {
                    MessageBox.Show("No username!");
                    return;
                }
                if (textBoxPassword.Text.Length <= 0)
                {
                    MessageBox.Show("No password!");
                    return;
                }
                string currentPassword = textBoxPassword.Text;
                SQLSContext context = new SQLSContext();
                var query = from curuser in context.Users
                            where curuser.Username == username
                            select curuser;
                if (!query.Any())
                {
                    return;
                }

            User selectedUser = query.First();
            context.Dispose();
                byte[]? passwordHash = instanceC.hashStringSalt(currentPassword, selectedUser.Salt);
                if (passwordHash == null)
                {
                    return;
                }
                instanceC.setAesFromPasswordStringForKey(currentPassword, selectedUser.Salt);
                bool result = instanceC.setRSAwithEncryptedFile(filePath, passwordHash);
                if (!result)
                {
                    MessageBox.Show("Wrong password!");
                    return;
                }
                byte[]? keyHash = instanceC.getPrivateKeyHash(selectedUser.Salt);
                if (keyHash == null)
                {
                    return;
                }
                if (!keyHash.SequenceEqual(selectedUser.KeyHash))
                {
                    MessageBox.Show("Wrong key file!");
                    return;
                }
                byte[] IV = instanceC.RSADecrypted(selectedUser.IV);
                instanceC.setAesFromPasswordStringForPasswords(currentPassword, selectedUser.Salt, IV);

                mainMenu = new MainMenu(selectedUser, firstmenu);
                this.Hide();
                mainMenu.Show();
            
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            reset();
            firstmenu.Show();
        }

        private void LoginMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            firstmenu.Close();
        }
    }
}
