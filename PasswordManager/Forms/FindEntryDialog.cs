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
    public partial class FindEntryDialog : Form
    {
        public FindEntryDialog()
        {
            InitializeComponent();
        }
        public string getName()
        {
            return textBoxName.Text;
        }
    }
}
