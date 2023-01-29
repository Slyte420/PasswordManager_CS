using Database.PasswordDB;
using PasswordManager.Crypto;
using PasswordManager.Forms;
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
        private MainMenu mainMenu = null!;
        private CryptoInstance instanceC= null!;
        private string? selectedFolder;
        public RegisterMenu(FirstMenu firstMenu)
        {
            InitializeComponent();
            this.firstMenu = firstMenu;
            
            
        }


        private void RegisterMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            firstMenu.Close();
        }

        private void reset()
        {
            textBoxUsername.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            selectedFolder = null;
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstMenu.Show();
            reset();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            instanceC = CryptoInstance.GetInstance();
            if (selectedFolder is null)
            {
                MessageBox.Show("No folder selected");
                return;
            }
            string username = textBoxUsername.Text;
            context = new SQLSContext();
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
                instanceC.placeRSAEncryptedFile(username,selectedFolder,passwordHash);
                instanceC.setAesFromPasswordStringForPasswords(password,salt);
                byte[]? keyHash = instanceC.getPrivateKeyHash(salt);
                byte[] encryptedIV = instanceC.getIVEncrypted();
                if(keyHash is null)
                {
                    return;
                }
                User currentUser = new User { Username = username,Password=passwordHash,Salt=salt,KeyHash=keyHash,IV=encryptedIV };
                context.Users.Add(currentUser);
                context.SaveChanges();
                context.Dispose();
                this.Hide();
                reset();
                mainMenu = new MainMenu(currentUser,firstMenu);
                mainMenu.Show();
            }
            else
            {
                MessageBox.Show("Username not available!");
            }
        }

        private void buttonSaveKey_Click(object sender, EventArgs e)
        {
            if(FolderBrowserDialogKey.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = FolderBrowserDialogKey.SelectedPath;
            }
        }
    }
}
