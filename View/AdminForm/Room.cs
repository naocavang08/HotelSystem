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
                // kiem tra du lieu dau vao
                if (string.IsNullOrEmpty(txbRoomNumber.Text) ||
                    cbbRoomType.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txbRoomNumber.Text, out int roomNumber))
                {
                    MessageBox.Show("Số phòng phải là số nguyên hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int roomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);

                using (var db = new DBHotelSystem())
                {
                    // kiem tra so phong da ton tai chua
                    bool isExists = db.Rooms.Any(r => r.room_number == roomNumber.ToString());
                    if (isExists)
                    {
                        MessageBox.Show("Số phòng đã tồn tại trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var newRoom = new Model.Room
                    {
                        room_number = roomNumber.ToString(),
                        status = "Available",
                        roomtype_id = roomTypeId
                    };

                    db.Rooms.Add(newRoom);
                    db.SaveChanges();
                }

                MessageBox.Show("Thêm phòng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadRoomData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // kiem tra da chon dong tren bang chua
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // kiem tra du lieu dau vao
                if (string.IsNullOrEmpty(txbRoomNumber.Text) ||
                    cbbRoomType.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txbRoomNumber.Text, out int roomNumber))
                {
                    MessageBox.Show("Số phòng phải là số nguyên hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["room_id"].Value);
                int roomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);

                using (var db = new DBHotelSystem())
                {
                    // kiem tra so phong da ton tai (tru phong dang sua)
                    bool isExists = db.Rooms.Any(r => r.room_number == roomNumber.ToString() && r.room_id != roomId);
                    if (isExists)
                    {
                        MessageBox.Show("Số phòng đã tồn tại trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var room = db.Rooms.FirstOrDefault(r => r.room_id == roomId);

                    if (room != null)
                    {
                        // cap nhat thong tin phong
                        room.room_number = roomNumber.ToString();
                        room.roomtype_id = roomTypeId;
                        room.status = txbStatus.Text.Trim();

                        db.SaveChanges();

                        MessageBox.Show("Cập nhật phòng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        LoadRoomData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // kiem tra da chon dong tren bang chua
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["room_id"].Value);

                    using (var db = new DBHotelSystem())
                    {
                        var room = db.Rooms.FirstOrDefault(r => r.room_id == roomId);

                        if (room != null)
                        {
                            db.Rooms.Remove(room);
                            db.SaveChanges();

                            MessageBox.Show("Xóa phòng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearFields();
                            LoadRoomData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //mục đích của db.Rooms.AsQueryable(); là để dùng các phương thức trên Rooms như Where, OrderBy, ...
                    IQueryable<Model.Room> baseQuery = db.Rooms.AsQueryable();

                    if (!string.IsNullOrEmpty(roomNumber))
                    {
                        baseQuery = baseQuery.Where(r => r.room_number.Contains(roomNumber));
                    }

                    // chi loc theo loai phong neu khac "Tat ca"
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
                        MessageBox.Show("Không tìm thấy phòng nào khớp với tiêu chí tìm kiếm", "Kết quả tìm kiếm",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadCBBRoomType()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    var roomTypes = db.RoomTypes.ToList();

                    // them muc "Tat ca" vao dau danh sach
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
                MessageBox.Show($"Lỗi khi tải loại phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txbRoomNumber.Text = string.Empty;
            cbbRoomType.Text = string.Empty;
            txbRoomPrice.Text = string.Empty;
            txbStatus.Text = string.Empty;
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
                MessageBox.Show($"Lỗi khi tải dữ liệu phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // lay du lieu tu dong duoc chon tren bang va hien thi len cac o nhap lieu
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txbRoomNumber.Text = row.Cells["room_number"].Value?.ToString() ?? string.Empty;
                cbbRoomType.Text = row.Cells["room_type"].Value?.ToString() ?? string.Empty;
                txbRoomPrice.Text = row.Cells["price"].Value?.ToString() ?? string.Empty;
                txbStatus.Text = row.Cells["status"].Value?.ToString() ?? string.Empty;
            }
        }
    }
}
