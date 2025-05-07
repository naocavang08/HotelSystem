using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HotelSystem.DTO;

namespace HotelSystem.DAL
{
    internal class AdminDAL
    {
        public void ConnectToDatabase()
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            // Create and open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection established successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
                }
            }
        }

        public void ExecuteQuery(string query)
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            // Create and open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create a SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) affected.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while executing the query: {ex.Message}");
                }
            }
        }

        public List<AdminDTO> GetAdmins()
        {
            var admins = new List<AdminDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT user_id, username, password FROM Users WHERE role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var admin = new AdminDTO(
                                    reader.GetInt32(0), // user_id
                                    reader.GetString(1), // username
                                    reader.GetString(2)  // password
                                );
                                admins.Add(admin);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving admins: {ex.Message}");
                }
            }

            return admins;
        }

        public void AddAdmin(AdminDTO admin)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (username, password, role, status, date_register) VALUES (@Username, @Password, 'Admin', 'Active', @DateRegister)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", admin.Username);
                        command.Parameters.AddWithValue("@Password", admin.Password);
                        command.Parameters.AddWithValue("@DateRegister", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} admin(s) added.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding an admin: {ex.Message}");
                }
            }
        }

        public void UpdateAdmin(AdminDTO admin)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET username = @Username, password = @Password WHERE user_id = @AdminId AND role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminId", admin.AdminId);
                        command.Parameters.AddWithValue("@Username", admin.Username);
                        command.Parameters.AddWithValue("@Password", admin.Password);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} admin(s) updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while updating an admin: {ex.Message}");
                }
            }
        }

        public void DeleteAdmin(int adminId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE user_id = @AdminId AND role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminId", adminId);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} admin(s) deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting an admin: {ex.Message}");
                }
            }
        }
    }
}
