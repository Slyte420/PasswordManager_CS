using PasswordManager.Forms;

namespace PasswordManager
{
    public partial class FirstMenu : Form
    {
        private LoginMenu loginMenu = null!;
        private RegisterMenu registerMenu = null!;
        public FirstMenu()
        {
            InitializeComponent();
            registerMenu = new RegisterMenu(this);
            loginMenu = new LoginMenu(this);
        }



        private void buttonRegister_Click(object sender, EventArgs e)
        {
            registerMenu.Show();
            this.Hide();
        }

        private void FirstMenu_Load(object sender, EventArgs e)
        {



        }

        private void FirstMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginMenu.Dispose();
            registerMenu.Dispose();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginMenu.Show();
        }
    }
}