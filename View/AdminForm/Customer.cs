using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using HotelSystem.Model;

namespace HotelSystem.View.AdminForm
{
    public partial class Customer : Form
    {
        private DBHotelSystem dbContext;

        public Customer()
        {
            InitializeComponent();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dbContext = new DBHotelSystem();

            // Đăng ký sự kiện load form
            this.Load += Customer_Load;

            // Đăng ký sự kiện cho DataGridView
            dataGridView1.CellClick += dataGridView1_CellClick;
        }


        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                // load lại dữ liệu từ DB
                dbContext.Customers.Load();
                dbContext.Bookings.Load();
                dbContext.Rooms.Load();

                // Lấy danh sách khách hàng
                var customers = dbContext.Customers.ToList();

                if (customers.Count > 0)
                {
                    // Tạo danh sách kết quả
                    var customerList = new List<object>();

                    foreach (var customer in customers)
                    {
                        // Tìm booking của customer nếu có
                        var bookings = dbContext.Bookings
                            .Where(b => b.customer_id == customer.customer_id)
                            .OrderByDescending(b => b.check_in)
                            .ToList();

                        if (bookings.Count > 0)
                        {
                            // Hiển thị thông tin customer với booking
                            foreach (var booking in bookings)
                            {
                                var room = dbContext.Rooms.FirstOrDefault(r => r.room_id == booking.room_id);

                                string status = "";
                                if (DateTime.Now >= booking.check_in && DateTime.Now <= booking.check_out)
                                    status = "Đang ở";
                                else if (DateTime.Now < booking.check_in)
                                    status = "Đã đặt";
                                else
                                    status = "Đã trả phòng";

                                customerList.Add(new
                                {
                                    customer.name,
                                    customer.phone,
                                    customer.cccd,
                                    check_in = booking.check_in,
                                    check_out = booking.check_out,
                                    room_number = room != null ? room.room_number : "",
                                    status = status,
                                    total_price = booking.total_price
                                });
                            }
                        }
                        else
                        {
                            // Hiển thị thông tin customer không có booking
                            customerList.Add(new
                            {
                                customer.name,
                                customer.phone,
                                customer.cccd,
                                check_in = (DateTime?)null,
                                check_out = (DateTime?)null,
                                room_number = "",
                                status = "",
                                total_price = (decimal)0
                            });
                        }
                    }

                    dataGridView1.DataSource = customerList;
                }
                else
                {
                    // Không có dữ liệu khách hàng
                    MessageBox.Show("Không có dữ liệu khách hàng trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }

                // Định dạng các cột
                FormatDataGridViewColumns();

                // Debug - Kiểm tra số lượng khách hàng
                Console.WriteLine($"Loaded {customers.Count} customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Debug - In lỗi chi tiết
                Console.WriteLine("LoadCustomerData Exception: " + ex.ToString());
            }
        }

        private void FormatDataGridViewColumns()
        {
            // Đặt tiêu đề cho các cột
            if (dataGridView1.Columns.Contains("name"))
                dataGridView1.Columns["name"].HeaderText = "Họ tên";

            if (dataGridView1.Columns.Contains("phone"))
                dataGridView1.Columns["phone"].HeaderText = "Số điện thoại";

            if (dataGridView1.Columns.Contains("cccd"))
                dataGridView1.Columns["cccd"].HeaderText = "CCCD";

            if (dataGridView1.Columns.Contains("check_in"))
            {
                dataGridView1.Columns["check_in"].HeaderText = "Ngày đến";
                dataGridView1.Columns["check_in"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dataGridView1.Columns.Contains("check_out"))
            {
                dataGridView1.Columns["check_out"].HeaderText = "Ngày đi";
                dataGridView1.Columns["check_out"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dataGridView1.Columns.Contains("room_number"))
                dataGridView1.Columns["room_number"].HeaderText = "Số phòng";

            if (dataGridView1.Columns.Contains("status"))
                dataGridView1.Columns["status"].HeaderText = "Trạng thái";

            if (dataGridView1.Columns.Contains("total_price"))
            {
                dataGridView1.Columns["total_price"].HeaderText = "Tổng tiền";
                dataGridView1.Columns["total_price"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["total_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Điều chỉnh độ rộng các cột
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Điền thông tin vào các textbox
                txtName.Text = row.Cells["name"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["phone"].Value?.ToString() ?? "";
                txtCCCD.Text = row.Cells["cccd"].Value?.ToString() ?? "";
            }
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtCCCD.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Sử dụng using để đảm bảo DbContext được dispose đúng cách
                using (var db = new DBHotelSystem())
                {
                    // Kiểm tra xem khách hàng đã tồn tại chưa
                    bool isExists = db.Customers.Any(c => c.phone == txtPhone.Text || c.cccd == txtCCCD.Text);

                    if (isExists)
                    {
                        MessageBox.Show("Khách hàng với số điện thoại hoặc CCCD này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo user cho khách hàng
                    User newUser = new User
                    {
                        username = txtPhone.Text,
                        password = txtPhone.Text, // Mặc định mật khẩu là số điện thoại
                        role = "customer",
                        date_register = DateTime.Now,
                        status = "active"
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Thêm khách hàng
                    Model.Customer newCustomer = new Model.Customer
                    {
                        name = txtName.Text,
                        phone = txtPhone.Text,
                        cccd = txtCCCD.Text,
                        id = newUser.id
                    };

                    db.Customers.Add(newCustomer);
                    db.SaveChanges();

                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Thêm khách hàng exception: " + ex.ToString());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string phone = dataGridView1.SelectedRows[0].Cells["phone"].Value.ToString();

                // Tìm khách hàng cần sửa
                var customer = dbContext.Customers.FirstOrDefault(c => c.phone == phone);

                if (customer != null)
                {
                    // Cập nhật thông tin
                    customer.name = txtName.Text;
                    customer.phone = txtPhone.Text;
                    customer.cccd = txtCCCD.Text;

                    // Lưu thay đổi
                    dbContext.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string phone = dataGridView1.SelectedRows[0].Cells["phone"].Value.ToString();

                    // Tìm khách hàng cần xóa
                    var customer = dbContext.Customers.FirstOrDefault(c => c.phone == phone);

                    if (customer != null)
                    {
                        // Lấy user_id
                        int userId = customer.id;

                        // Xóa khách hàng 
                        dbContext.Customers.Remove(customer);
                        dbContext.SaveChanges();

                        // Tìm và xóa user
                        var user = dbContext.Users.Find(userId);
                        if (user != null)
                        {
                            dbContext.Users.Remove(user);
                            dbContext.SaveChanges();
                        }

                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        LoadCustomerData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchTerm = "";

            if (!string.IsNullOrWhiteSpace(txtName.Text))
                searchTerm = txtName.Text;
            else if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                searchTerm = txtPhone.Text;
            else if (!string.IsNullOrWhiteSpace(txtCCCD.Text))
                searchTerm = txtCCCD.Text;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadCustomerData();
                return;
            }

            try
            {
                var query = from c in dbContext.Customers
                            join b in dbContext.Bookings on c.customer_id equals b.customer_id into bookings
                            from b in bookings.DefaultIfEmpty()
                            join r in dbContext.Rooms on b != null ? b.room_id : 0 equals r.room_id into rooms
                            from r in rooms.DefaultIfEmpty()
                            where c.name.Contains(searchTerm) || c.phone.Contains(searchTerm) || c.cccd.Contains(searchTerm)
                            select new
                            {
                                c.name,
                                c.phone,
                                c.cccd,
                                check_in = b != null ? b.check_in : (DateTime?)null,
                                check_out = b != null ? b.check_out : (DateTime?)null,
                                room_number = r != null ? r.room_number : null,
                                status = b == null ? "" :
                                       (DateTime.Now >= b.check_in && DateTime.Now <= b.check_out ? "Đang ở" :
                                       DateTime.Now < b.check_in ? "Đã đặt" : "Đã trả phòng"),
                                total_price = b != null ? b.total_price : 0
                            };

                dataGridView1.DataSource = query.ToList();
                FormatDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }


    }
}
