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
    public partial class EditUserForm : Form
    {
        private Dashboard Dashboard;
        private EditUserFormService Service;
        private string Id;
        public EditUserForm(Dashboard dashboard, string id)
        {
            Dashboard = dashboard;
            Id = id;
            InitializeComponent();
            LoadTextBoxes(id);
        }

        private async Task LoadTextBoxes(string id)
        {
            Service = new EditUserFormService();

            User user = await Service.GetUser(id);

            userLabel.Text = $"Editing {user.Name} information.";

            nameTextBox.Text = user.Name;

            ageTextBox.Text = user.Age.ToString();

            addressTextBox.Text = user.Address;
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var isSuccessful = await Service.UpdateUser(int.Parse(Id), nameTextBox.Text, ageTextBox.Text, addressTextBox.Text);
                if (isSuccessful)
                {
                    await Dashboard.TryRefresh();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var isSuccessful = await Service.DeleteUser(Id);
                if (isSuccessful)
                {
                    await Dashboard.TryRefresh();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
