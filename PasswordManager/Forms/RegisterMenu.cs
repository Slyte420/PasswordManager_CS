using Database;
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
        public RegisterMenu(SQLSContext context,FirstMenu firstMenu)
        {
            InitializeComponent();
            this.firstMenu = firstMenu;
            this.context = context;
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
            string Username = textBoxUsername.Text;
            var queryUsername = from user in context.Users
                                where user.UserName== Username
                                select user.UserName;
            if (!queryUsername.Any())
            {
                string Password = textBoxPassword.Text;
                //Hash Function
            }
        }
    }
}
