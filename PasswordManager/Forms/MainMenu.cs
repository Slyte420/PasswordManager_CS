using Database;
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
    public partial class MainMenu : Form
    {
        private SQLSContext context = null!;
        public MainMenu()
        {
            InitializeComponent();
            context= new SQLSContext();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            var QueryGroup = from gr in context.EntryGroups
                        select gr;
            var GroupList = QueryGroup.ToList();
            comboBoxGroup.Items.Add("All");
            comboBoxGroup.SelectedIndex= 0;
            foreach(var gr in QueryGroup) {    
                comboBoxGroup.Items.Add(gr.Name);
            }
            var QueryEntry = from entry in context.Entries
                             select entry;
            dataGridViewData.DataSource = QueryEntry.ToList();
        }
    }
}
