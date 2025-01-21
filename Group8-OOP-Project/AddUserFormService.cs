using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group8_OOP_Project
{
    internal class AddUserFormService
    {
        internal async Task AddUser(string name, string age, string address)
        {
            var user = new User
            {
                Name = name,
                Age = int.Parse(age),
                Address = address
            };

            var validator = new UserValidator();
            if (!validator.ValidateUser(user))
            {
                throw new Exception("Invalid user");
            }

            var service = new MySqlService();
            await service.AddUser(user);

            MessageBox.Show("User added successfully");
        }
    }
}
