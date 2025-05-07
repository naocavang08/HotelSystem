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
        // New method to fetch occupied rooms
        public List<string> GetOccupiedRooms()
        {
            var occupiedRooms = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT status FROM Rooms WHERE status = 'in use'";
                    // gia su status = "in use" la phong da co nguoi dat

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                occupiedRooms.Add(reader.GetString(0)); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving occupied rooms: {ex.Message}");
                }
            }

            return occupiedRooms;
        }

        //New method to fetch available rooms
        public List<string> GetAvailableRooms()
        {
            var availableRooms = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT status FROM Rooms WHERE status = 'available'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                availableRooms.Add(reader.GetString(0)); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving available rooms: {ex.Message}");
                }
            }

            return availableRooms;
        }

        //New method to fetch all customers
        public List<int> GetAllCustomers()
        {
            var customers = new List<int>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT customer_id FROM Customers";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(reader.GetInt32(0)); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving customers: {ex.Message}");
                }
            }
            return customers;
        }

        //New method to get all Revenue
        public decimal GetAllRevenue()
        {
            decimal totalRevenue = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT SUM(total_amount) 
                FROM Invoice 
                WHERE payment_date BETWEEN '2025-07-01' AND '2025-07-31'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalRevenue = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving revenue: {ex.Message}");
                }
            }

            return totalRevenue;
        }
















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
                    string query = "SELECT user_id, username, password, role, status, date_register FROM Users WHERE role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var admin = new AdminDTO(
                                    reader.GetInt32(0), // user_id
                                    reader.GetString(1), // username
                                    reader.GetString(2), // password
                                    reader.GetString(3), // role
                                    reader.GetString(4), // status
                                    reader.GetDateTime(5) // date_register
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
