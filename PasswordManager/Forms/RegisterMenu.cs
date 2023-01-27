using Database;
using PasswordManager.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class RegisterMenu : Form
    {
        private SQLSContext context = null!;
        private FirstMenu firstMenu = null!;
        private CryptoInstance instanceC= null!;
        private string? selectedFolder;
        public RegisterMenu(SQLSContext context,FirstMenu firstMenu)
        {
            InitializeComponent();
            this.firstMenu = firstMenu;
            this.context = context;
            instanceC = CryptoInstance.GetInstance();
        }


        private void RegisterMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            firstMenu.Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstMenu.Show();
            
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(selectedFolder is null)
            {
                MessageBox.Show("No folder selected");
                return;
            }
            string username = textBoxUsername.Text;
            var queryUsername = from user in context.Users
                                where user.Username== username
                                select user.Username;
            if (!queryUsername.Any())
            {
                string password = textBoxPassword.Text;
                byte[] salt = instanceC.generateSalt(32);
                byte[]? passwordHash = instanceC.hashStringSalt(password, salt);
                if(passwordHash is null) {
                    return;
                }
                instanceC.setAesFromPasswordStringForKey(password,salt);
                instanceC.getRSAEncryptedFile(username,selectedFolder,passwordHash);
                instanceC.setAesFromPasswordStringForPasswords(password,salt);
                byte[]? keyHash = instanceC.getPrivateKeyHash(salt);
                byte[] encryptedIV = instanceC.getIVEncrypted();
                if(keyHash is null)
                {
                    return;
                }
                User currentUser = new User { Username = username,Password=passwordHash,Salt=salt,keyHash=keyHash,IV=encryptedIV };
                context.Users.Add(currentUser);
                context.SaveChanges();
                this.Hide();
                //Main menu form
            }
        }

        private void buttonSaveKey_Click(object sender, EventArgs e)
        {
            if(keyFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = keyFolderBrowserDialog.SelectedPath;
            }
        }
    }
}
