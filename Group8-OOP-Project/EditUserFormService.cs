using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group8_OOP_Project
{
    internal class EditUserFormService
    {
        public async Task<bool> UpdateUser (int id, string name, string age, string address)
        {

            MySqlService mySqlService = new MySqlService();
            User user = new User()
            {
                Id = id,
                Name = name,
                Age = int.Parse(age),
                Address = address
            };

            var validator = new UserValidator();
            if (validator.ValidateUser(user) == false)
            {
                throw new Exception("Invalid user information");
            }

            if (MessageBox.Show("Are you sure you want to update this user?", "Update User", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }

            await mySqlService.UpdateUser(user);

            MessageBox.Show("User information updated successfully");

            return true;
        }

        public async Task<User> GetUser(string id)
        {
            MySqlService mySqlService = new MySqlService();
            return await mySqlService.GetUserById(int.Parse(id));
        }

        public async Task<bool> DeleteUser(string id)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }

            MySqlService mySqlService = new MySqlService();

            await mySqlService.DeleteUser(int.Parse(id));

            MessageBox.Show("User deleted successfully");

            return true;
        }
    }
}
