using Database.PasswordDB;
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
            if(textBoxName.Text.Length == 0)
            {
                return null;
            }
            if(exist == null)
            {
                exist = new EntryGroup { Name = textBoxName.Text };
            }
            else
            {
                exist.Name= textBoxName.Text;
            }
            return exist;
        }
    }
}
