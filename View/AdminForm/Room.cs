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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadCBBRoomType();
            LoadRoomData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txbRoomNumber.Text) ||
                    cbbRoomType.SelectedValue == null)
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

                // Get room type ID from the selected value of the combo box
                int roomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);

                // Add room to database using Entity Framework
                using (var db = new DBHotelSystem())
                {
                    var newRoom = new Model.Room
                    {
                        room_number = roomNumber.ToString(),
                        status = "Available",
                        roomtype_id = roomTypeId
                    };

                    db.Rooms.Add(newRoom);
                    db.SaveChanges();
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
                    cbbRoomType.SelectedValue == null)
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

                // Get room ID from the selected row
                int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["room_id"].Value);

                // Get room type ID from the selected value of the combo box
                int roomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);

                // Update room in database using Entity Framework
                using (var db = new DBHotelSystem())
                {
                    // Find the room to update
                    var room = db.Rooms.FirstOrDefault(r => r.room_id == roomId);

                    if (room != null)
                    {
                        // Update room properties
                        room.room_number = roomNumber.ToString();
                        room.roomtype_id = roomTypeId;

                        // Save changes to database
                        db.SaveChanges();

                        MessageBox.Show("Room updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear input fields
                        ClearFields();

                        // Refresh the data grid
                        LoadRoomData();
                    }
                    else
                    {
                        MessageBox.Show("Room not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                    // Get room ID from the selected row
                    int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["room_id"].Value);

                    // Delete room from database using Entity Framework
                    using (var db = new DBHotelSystem())
                    {
                        // Find the room to delete
                        var room = db.Rooms.FirstOrDefault(r => r.room_id == roomId);

                        if (room != null)
                        {
                            // Remove room from database
                            db.Rooms.Remove(room);

                            // Save changes to database
                            db.SaveChanges();

                            MessageBox.Show("Room deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear input fields
                            ClearFields();

                            // Refresh the data grid
                            LoadRoomData();
                        }
                        else
                        {
                            MessageBox.Show("Room not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
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
                string roomNumber = txbRoomNumber.Text.Trim();
                int? roomTypeId = cbbRoomType.SelectedValue != null ? Convert.ToInt32(cbbRoomType.SelectedValue) : (int?)null;

                using (var db = new DBHotelSystem())
                {
                    IQueryable<Model.Room> baseQuery = db.Rooms.AsQueryable();

                    if (!string.IsNullOrEmpty(roomNumber))
                    {
                        baseQuery = baseQuery.Where(r => r.room_number.Contains(roomNumber));
                    }

                    // Chỉ lọc theo loại phòng nếu không phải "Tất cả" (tức roomtype_id != 0)
                    if (roomTypeId.HasValue && roomTypeId.Value != 0)
                    {
                        baseQuery = baseQuery.Where(r => r.roomtype_id == roomTypeId.Value);
                    }

                    var results = (from r in baseQuery
                                   join rt in db.RoomTypes on r.roomtype_id equals rt.roomtype_id
                                   select new
                                   {
                                       r.room_id,
                                       r.room_number,
                                       r.status,
                                       rt.room_type,
                                       rt.price
                                   }).ToList();

                    dataGridView1.DataSource = results;
                    dataGridView1.Refresh();

                    if (results.Count == 0)
                    {
                        MessageBox.Show("No rooms found matching the search criteria", "Search Results",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching rooms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadCBBRoomType()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    var roomTypes = db.RoomTypes.ToList();

                    // Thêm mục "Tất cả" vào đầu danh sách
                    roomTypes.Insert(0, new RoomType
                    {
                        roomtype_id = 0,
                        room_type = "Tất cả"
                    });

                    cbbRoomType.DataSource = roomTypes;
                    cbbRoomType.DisplayMember = "room_type";
                    cbbRoomType.ValueMember = "roomtype_id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txbRoomNumber.Text = string.Empty;
            cbbRoomType.Text = string.Empty;
            txbRoomPrice.Text = string.Empty;
            txbRoomNumber.Focus();
        }

        private void LoadRoomData()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    var rooms = (from r in db.Rooms
                                 join rt in db.RoomTypes on r.roomtype_id equals rt.roomtype_id
                                 select new
                                 {
                                     r.room_id,
                                     r.room_number,
                                     r.status,
                                     rt.room_type,
                                     rt.price
                                 }).ToList();

                    dataGridView1.DataSource = rooms;
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadRoomData();
        }
    }
}
