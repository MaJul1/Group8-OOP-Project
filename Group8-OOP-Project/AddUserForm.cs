using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group8_OOP_Project
{
    public partial class AddUserForm : Form
    {
        private readonly AddUserFormService addUserFormService = new AddUserFormService();
        private readonly Dashboard Dashboard;
        public AddUserForm(Dashboard dashboard)
        {
            Dashboard = dashboard;
            InitializeComponent();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                await addUserFormService.AddUser(nameTextBox.Text, ageTextBox.Text, addressTextBox.Text);
                await Dashboard.TryRefresh();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
