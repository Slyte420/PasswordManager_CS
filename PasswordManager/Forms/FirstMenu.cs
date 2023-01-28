using Database.PasswordDB;

namespace PasswordManager
{
    public partial class FirstMenu : Form
    {
        private SQLSContext context = null!;
        private RegisterMenu registerMenu= null!;
        public FirstMenu()
        {
            InitializeComponent();
            registerMenu = new RegisterMenu(context,this);
        }

       

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            
            registerMenu.Show();
            this.Hide();
        }

        private void FirstMenu_Load(object sender, EventArgs e)
        {

            context= new SQLSContext();
            
        }

        private void FirstMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.context?.Dispose();
        }
    }
}