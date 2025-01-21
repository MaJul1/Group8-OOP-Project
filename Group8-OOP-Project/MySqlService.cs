using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Group8_OOP_Project
{
    internal class MySqlService
    {
        private string connectionString = "server=localhost;port=3306;user=root;database=oop_project;pwd=password";

        internal async Task<List<User>> GetAllUser()
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users", connection))
                {
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Address = reader.GetString(3)
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        internal async Task<User> GetUserById(int id)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Address = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return user;
        }

        internal async Task AddUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO users (name, age, address) VALUES (@name, @age, @address)", connection))
                {
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@address", user.Address);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        internal async Task UpdateUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("UPDATE users SET name = @name, age = @age, address = @address WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@address", user.Address);
                    command.Parameters.AddWithValue("@id", user.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        internal async Task DeleteUser(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM users WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        internal async Task<bool> Exists(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM users WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return (long)await command.ExecuteScalarAsync() > 0;
                }
            }
        }

        public string GetConnectionString()
        {
            return connectionString;
        }

        internal static bool TestConnection()
        {
            var MySqlService = new MySqlService();
            var connectionString = MySqlService.GetConnectionString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show($"Unable to connect to database \n ConnectionString: {connectionString}");
                    return false;
                }
            }
        }
    }
}
