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

        // Tai du lieu dich vu tu database
        private void LoadServiceData()
        {
            try
            {
                dbContext.Services.Load();
                var services = dbContext.Services.ToList();

                if (services.Count > 0)
                {
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
                    MessageBox.Show("Không có dữ liệu dịch vụ trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }

                FormatDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Dinh dang hien thi cac cot trong bang du lieu
        private void FormatDataGridViewColumns()
        {
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

            dataGridView1.AutoResizeColumns();
        }

        // Xu ly su kien khi click vao mot dong trong bang
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtName.Text = row.Cells["name"].Value?.ToString() ?? "";

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

        // Xoa trang cac truong nhap lieu
        private void ClearFields()
        {
            txtName.Text = "";
            txtPrice.Text = "";
        }

        // Xu ly su kien them dich vu
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiem tra gia dich vu
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số dương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiem tra dich vu da ton tai chua
                var existingService = dbContext.Services.FirstOrDefault(s => s.name == txtName.Text);
                if (existingService != null)
                {
                    MessageBox.Show("Dịch vụ này đã tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DBHotelSystem())
                {
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
            }
        }

        // Xu ly su kien sua thong tin dich vu
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

            // Kiem tra gia dich vu
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số dương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["service_id"].Value);
                var service = dbContext.Services.Find(serviceId);

                if (service != null)
                {
                    service.name = txtName.Text;
                    service.price = price;
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

        // Xu ly su kien xoa dich vu
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
                    var service = dbContext.Services.Find(serviceId);

                    if (service != null)
                    {
                        // Kiem tra neu dich vu da duoc dat
                        var hasBookings = dbContext.BookingServices.Any(b => b.service_id == service.service_id);

                        if (hasBookings)
                        {
                            MessageBox.Show("Không thể xóa dịch vụ này vì có dữ liệu đặt dịch vụ liên quan.",
                                "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

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

        // Xu ly su kien tim kiem dich vu
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

        // Tai lai du lieu
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadServiceData();
        }

        // Giai phong tai nguyen khi dong form
        private void Service_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
