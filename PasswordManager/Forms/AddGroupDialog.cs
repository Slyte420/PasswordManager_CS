using Database.PasswordDB;

namespace PasswordManager.Forms
{
    public partial class AddGroupDialog : Form
    {
        private EntryGroup? exist;
        public AddGroupDialog()
        {
            InitializeComponent();
        }
        public AddGroupDialog(EntryGroup group)
        {
            InitializeComponent();
            exist = group;
        }
        public EntryGroup? getGroup()
        {
            if (textBoxName.Text.Length == 0)
            {
                return null;
            }
            if (exist == null)
            {
                exist = new EntryGroup { Name = textBoxName.Text };
            }
            else
            {
                exist.Name = textBoxName.Text;
            }
            return exist;
        }
    }
}
