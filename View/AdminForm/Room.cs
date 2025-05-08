using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using HotelSystem.Model;


namespace HotelSystem.View.AdminForm
{
    public partial class Room : Form
    {
        public Room()
        {
            InitializeComponent();
            LoadRoomData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txbRoomNumber.Text) ||
                    string.IsNullOrEmpty(txbRoomType.Text) ||
                    string.IsNullOrEmpty(txbRoomPrice.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse room number
                if (!int.TryParse(txbRoomNumber.Text, out int roomNumber))
                {
                    MessageBox.Show("Room number must be a valid integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse room type (roomtype_id is an integer !?)
                if (!int.TryParse(txbRoomType.Text, out int roomTypeId))
                {
                    MessageBox.Show("Room type must be a valid integer (roomtype_id)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse room price
                if (!decimal.TryParse(txbRoomPrice.Text, out decimal roomPrice))
                {
                    MessageBox.Show("Room price must be a valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add room to database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Rooms (room_number, status, roomtype_id) VALUES (@RoomNumber, @Status, @RoomTypeId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                        command.Parameters.AddWithValue("@Status", "Available"); // Default status
                        command.Parameters.AddWithValue("@RoomTypeId", roomTypeId);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Room added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                ClearFields();

                // Refresh the data grid
                LoadRoomData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected in the data grid
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a room to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate input fields
                if (string.IsNullOrEmpty(txbRoomNumber.Text) ||
                    string.IsNullOrEmpty(txbRoomType.Text) ||
                    string.IsNullOrEmpty(txbRoomPrice.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse room number and price
                if (!int.TryParse(txbRoomNumber.Text, out int roomNumber))
                {
                    MessageBox.Show("Room number must be a valid integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txbRoomPrice.Text, out decimal roomPrice))
                {
                    MessageBox.Show("Room price must be a valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update room in database logic would go here
                // Example: roomService.UpdateRoom(roomId, roomNumber, txbRoomType.Text, roomPrice);

                MessageBox.Show("Room updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                ClearFields();

                // Refresh the data grid
                // LoadRoomData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected in the data grid
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a room to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this room?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected room ID
                    // int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RoomId"].Value);

                    // Delete room from database logic would go here
                    // Example: roomService.DeleteRoom(roomId);

                    MessageBox.Show("Room deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear input fields
                    ClearFields();

                    // Refresh the data grid
                    // LoadRoomData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Get search criteria
                string roomNumber = txbRoomNumber.Text.Trim();
                string roomType = txbRoomType.Text.Trim();
                string roomPrice = txbRoomPrice.Text.Trim();

                // Search logic would go here
                // Example: var results = roomService.SearchRooms(roomNumber, roomType, roomPrice);

                // Update the data grid with search results
                // dataGridView1.DataSource = results;

                // If no search criteria provided, load all rooms
                if (string.IsNullOrEmpty(roomNumber) &&
                    string.IsNullOrEmpty(roomType) &&
                    string.IsNullOrEmpty(roomPrice))
                {
                    // LoadRoomData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching rooms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txbRoomNumber.Text = string.Empty;
            txbRoomType.Text = string.Empty;
            txbRoomPrice.Text = string.Empty;
            txbRoomNumber.Focus();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txbRoomNumber.Text = row.Cells["RoomNumber"].Value.ToString();
                txbRoomType.Text = row.Cells["RoomType"].Value.ToString();
                txbRoomPrice.Text = row.Cells["RoomPrice"].Value.ToString();
            }
        }

        private void LoadRoomData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBHotelSystem"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT room_id, room_number, status FROM Rooms";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
