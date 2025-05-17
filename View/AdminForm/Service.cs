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
    public partial class Service : Form
    {
        private DBHotelSystem dbContext;

        public Service()
        {
            InitializeComponent();
            dbContext = new DBHotelSystem();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Service_Load(object sender, EventArgs e)
        {
            LoadServiceData();
        }

        private void LoadServiceData()
        {
            try
            {
                // load lại dữ liệu từ DB
                dbContext.Services.Load();

                // Lấy danh sách dịch vụ
                var services = dbContext.Services.ToList();

                if (services.Count > 0)
                {
                    // Tạo danh sách kết quả hiển thị
                    var serviceList = services.Select(s => new
                    {
                        s.service_id,
                        s.name,
                        price = s.price,
                        booking_count = s.BookingServices.Count
                    }).ToList();

                    dataGridView1.DataSource = serviceList;
                }
                else
                {
                    // Không có dữ liệu dịch vụ
                    MessageBox.Show("Không có dữ liệu dịch vụ trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }

                // Định dạng các cột
                FormatDataGridViewColumns();

                // Debug - Kiểm tra số lượng dịch vụ
                Console.WriteLine($"Loaded {services.Count} services");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Debug - In lỗi chi tiết
                Console.WriteLine("LoadServiceData Exception: " + ex.ToString());
            }
        }

        private void FormatDataGridViewColumns()
        {
            // Đặt tiêu đề cho các cột
            if (dataGridView1.Columns.Contains("service_id"))
                dataGridView1.Columns["service_id"].HeaderText = "Mã dịch vụ";

            if (dataGridView1.Columns.Contains("name"))
                dataGridView1.Columns["name"].HeaderText = "Tên dịch vụ";

            if (dataGridView1.Columns.Contains("price"))
            {
                dataGridView1.Columns["price"].HeaderText = "Giá dịch vụ";
                dataGridView1.Columns["price"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridView1.Columns.Contains("booking_count"))
                dataGridView1.Columns["booking_count"].HeaderText = "Số lượt đặt";

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
                
                // Đảm bảo giá được hiển thị đúng định dạng
                if (decimal.TryParse(row.Cells["price"].Value?.ToString(), out decimal price))
                {
                    txtPrice.Text = price.ToString();
                }
                else
                {
                    txtPrice.Text = "0";
                }
            }
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtPrice.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra giá dịch vụ là số hợp lệ
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số dương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra dịch vụ đã tồn tại chưa (theo tên)
                var existingService = dbContext.Services.FirstOrDefault(s => s.name == txtName.Text);
                if (existingService != null)
                {
                    MessageBox.Show("Dịch vụ này đã tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DBHotelSystem())
                {
                    // Thêm dịch vụ mới
                    Model.Service newService = new Model.Service
                    {
                        name = txtName.Text,
                        price = price
                    };

                    db.Services.Add(newService);
                    db.SaveChanges();

                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadServiceData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Thêm dịch vụ exception: " + ex.ToString());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra giá dịch vụ là số hợp lệ
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số dương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["service_id"].Value);

                // Tìm dịch vụ cần sửa
                var service = dbContext.Services.Find(serviceId);

                if (service != null)
                {
                    // Cập nhật thông tin dịch vụ
                    service.name = txtName.Text;
                    service.price = price;

                    // Lưu thay đổi
                    dbContext.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadServiceData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["service_id"].Value);

                    // Tìm dịch vụ cần xóa
                    var service = dbContext.Services.Find(serviceId);

                    if (service != null)
                    {
                        // Kiểm tra nếu dịch vụ đã được đặt
                        var hasBookings = dbContext.BookingServices.Any(b => b.service_id == service.service_id);
                        
                        if (hasBookings)
                        {
                            MessageBox.Show("Không thể xóa dịch vụ này vì có dữ liệu đặt dịch vụ liên quan.", 
                                "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        // Xóa dịch vụ
                        dbContext.Services.Remove(service);
                        dbContext.SaveChanges();

                        MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        LoadServiceData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchTerm = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadServiceData();
                return;
            }

            try
            {
                var query = from s in dbContext.Services
                            where s.name.Contains(searchTerm)
                            select new
                            {
                                s.service_id,
                                s.name,
                                price = s.price,
                                booking_count = s.BookingServices.Count
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
            LoadServiceData();
        }

        private void Service_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
