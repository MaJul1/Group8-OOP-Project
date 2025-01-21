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
    public partial class Dashboard : Form
    {
        private readonly DashboardService Service;
        public Dashboard()
        {
            Service = new DashboardService();
            InitializeComponent();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                await Service.LoadUsersToListView(userListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void userListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            editButton.Visible = userListView.SelectedItems.Count == 1;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new AddUserForm(this).Show();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            await TryRefresh();
        }

        public async Task TryRefresh()
        {
            try
            {
                editButton.Visible = false;
                userListView.Clear();
                await Service.LoadUsersToListView(userListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                new EditUserForm(this, userListView.SelectedItems[0].SubItems[0].Text).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
