using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8_OOP_Project
{
    internal class UserValidator
    {
        public bool ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || user.Name.Length < 0)
            {
                return false;
            }
            if (user.Age < 0 || user.Age > 200)
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Name) || user.Name.Length < 0)
            {
                return false;
            }
            return true;
        }
    }
}
