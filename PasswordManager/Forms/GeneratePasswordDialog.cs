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
    public partial class GeneratePasswordDialog : Form
    {
        public GeneratePasswordDialog()
        {
            InitializeComponent();
            numericUpDownLength.Maximum = RNGPassword.getMax();
            numericUpDownLength.Minimum = RNGPassword.getMin();
        }

        public string generatePassword() 
        {
            RNGPassword instance = new RNGPassword(checkBoxUpperCase.Checked, checkBoxLowerCase.Checked, checkBoxNumbers.Checked, checkBoxSpecial.Checked);
            string? result = instance.generatePassword(Convert.ToInt32(numericUpDownLength.Value));
            if(result == null)
            {
               return string.Empty;
            }
            else
            {
                return result;
            }
        }
    }
}
