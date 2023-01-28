using Database.PasswordDB;
using Microsoft.EntityFrameworkCore;
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
        private User? user;
        private List<Entry> entries= null!;
        private List<EntryGroup> groups = null!;
        int[] columnsDisabled = {0,6,7,8,9 };
        public MainMenu()
        {
            InitializeComponent();
            
            
        }

        private async void MainMenu_Load(object sender, EventArgs e)
        {
            context = new SQLSContext();
            user = (from users in context.Users
                    select users).First();
            var taskGroups = AsyncOperationsPassDB.getGroupsAsync(user.Id);

            groups = await taskGroups;
            
            var taskEntries = AsyncOperationsPassDB.getEntriesAsync(user.Id);
            comboBoxGroup.Items.Add("All");
            comboBoxGroup.SelectedIndex= 0;
            foreach(var gr in groups) {    
                comboBoxGroup.Items.Add(gr.Name);
            }
            entries = await taskEntries;
            dataGridViewData.DataSource = entries;
            foreach (int i in columnsDisabled)
            {
                dataGridViewData.Columns[i].Visible = false;
            }
           
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(entries == null)
            {
                return;
            }
            int index = comboBoxGroup.SelectedIndex - 1;
            if (index >= 0)
            {
                int groupId = groups[index].Id;
                var query = from entry in entries
                            where entry.GroupId == groupId
                            select entry;
                dataGridViewData.DataSource = query.ToList();
            }
            else
            {
                dataGridViewData.DataSource = entries;
                foreach (int i in columnsDisabled)
                {
                    dataGridViewData.Columns[i].Visible = false;
                }
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            User? user = null;
            using (AddEditEntryDialog dialog = new AddEditEntryDialog())
            {
                dialog.Text = "Add";
                if(dialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
            if(user == null)
            {
                return;
            }
            
        }
    }
}
