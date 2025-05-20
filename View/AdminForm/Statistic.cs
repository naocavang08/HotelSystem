using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.BLL;
using HotelSystem.Model;

namespace HotelSystem.View.AdminForm
{
    public partial class Statistic : Form
    {
        private DBHotelSystem db = new DBHotelSystem();

        public Statistic()
        {
            InitializeComponent();
            LoadComboBoxes();
            cbbLoaiThongKe.SelectedIndex = 0; // Default to monthly statistics
        }

        private void LoadComboBoxes()
        {
            // Load months (1-12)
            for (int i = 1; i <= 12; i++)
            {
                cbbThang.Items.Add(i);
            }
            cbbThang.SelectedIndex = DateTime.Now.Month - 1; // Current month

            // Load quarters (1-4)
            for (int i = 1; i <= 4; i++)
            {
                cbbQuy.Items.Add($"Quý {i}");
            }
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            cbbQuy.SelectedIndex = currentQuarter - 1;

            // Load years (current year and 4 previous years)
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                cbbNam.Items.Add(currentYear - i);
            }
            cbbNam.SelectedIndex = 0; // Current year

            // Load statistic types
            cbbLoaiThongKe.Items.Add("Theo tháng");
            cbbLoaiThongKe.Items.Add("Theo quý");
            cbbLoaiThongKe.Items.Add("Theo năm");
        }

        private void cbbLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đặt màu mặc định cho các panel (giống Designer)
            panelThang.BackColor = Color.FromArgb(64, 64, 64);
            panelQuy.BackColor = Color.FromArgb(64, 64, 64);
            panelNam.BackColor = Color.FromArgb(64, 64, 64);

            // Đặt màu chữ cho tất cả label trong các panel là WhiteSmoke
            SetPanelLabelColor(panelThang, Color.WhiteSmoke);
            SetPanelLabelColor(panelQuy, Color.WhiteSmoke);
            SetPanelLabelColor(panelNam, Color.WhiteSmoke);

            // Hiển thị panel tương ứng và đổi màu nền sang đen khi được chọn
            panelThang.Visible = cbbLoaiThongKe.SelectedIndex == 0;
            panelQuy.Visible = cbbLoaiThongKe.SelectedIndex == 1;
            panelNam.Visible = cbbLoaiThongKe.SelectedIndex == 2;

            if (panelThang.Visible) panelThang.BackColor = Color.Black;
            if (panelQuy.Visible) panelQuy.BackColor = Color.Black;
            if (panelNam.Visible) panelNam.BackColor = Color.Black;

            if (cbbLoaiThongKe.SelectedIndex == 0) UpdateThongKeThang();
            else if (cbbLoaiThongKe.SelectedIndex == 1) UpdateThongKeQuy();
            else if (cbbLoaiThongKe.SelectedIndex == 2) UpdateThongKeNam();
        }

        // Hàm phụ để đổi màu chữ cho tất cả Label trong panel
        private void SetPanelLabelColor(Panel panel, Color color)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = color;
                }
            }
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiThongKe.SelectedIndex == 0)
                UpdateThongKeThang();
        }

        private void cbbQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiThongKe.SelectedIndex == 1)
                UpdateThongKeQuy();
        }

        private void cbbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiThongKe.SelectedIndex == 2)
                UpdateThongKeNam();
        }

        private void UpdateThongKeThang()
        {
            try
            {
                int selectedMonth = cbbThang.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                // Get all bookings for selected month in current year
                var monthlyBookings = db.Bookings
                    .Where(b => b.check_in.Month == selectedMonth && b.check_in.Year == currentYear)
                    .ToList();

                // Get all invoices for selected month in current year
                var monthlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                           i.payment_date.Value.Month == selectedMonth &&
                           i.payment_date.Value.Year == currentYear)
                    .ToList();

                // Calculate statistics
                int totalBookings = monthlyBookings.Count;
                decimal totalRevenue = monthlyInvoices.Sum(i => i.total_amount);

                // Update UI
                lblTongHoaDonThang.Text = totalBookings.ToString();
                lblTongDoanhThuThang.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê tháng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThongKeQuy()
        {
            try
            {
                int selectedQuarter = cbbQuy.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                // Calculate start and end months for the selected quarter
                int startMonth = (selectedQuarter - 1) * 3 + 1;
                int endMonth = startMonth + 2;

                // Get all bookings for selected quarter in current year
                var quarterlyBookings = db.Bookings
                    .Where(b => b.check_in.Month >= startMonth &&
                               b.check_in.Month <= endMonth &&
                               b.check_in.Year == currentYear)
                    .ToList();

                // Get all invoices for selected quarter in current year
                var quarterlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                               i.payment_date.Value.Month >= startMonth &&
                               i.payment_date.Value.Month <= endMonth &&
                               i.payment_date.Value.Year == currentYear)
                    .ToList();

                // Calculate statistics
                int totalBookings = quarterlyBookings.Count;
                decimal totalRevenue = quarterlyInvoices.Sum(i => i.total_amount);

                // Update UI
                lblTongHoaDonQuy.Text = totalBookings.ToString();
                lblTongDoanhThuQuy.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê quý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThongKeNam()
        {
            try
            {
                int selectedYear = Convert.ToInt32(cbbNam.SelectedItem);

                // Get all bookings for selected year
                var yearlyBookings = db.Bookings
                    .Where(b => b.check_in.Year == selectedYear)
                    .ToList();

                // Get all invoices for selected year
                var yearlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                               i.payment_date.Value.Year == selectedYear)
                    .ToList();

                // Calculate statistics
                int totalBookings = yearlyBookings.Count;
                decimal totalRevenue = yearlyInvoices.Sum(i => i.total_amount);

                // Update UI
                lblTongHoaDonNam.Text = totalBookings.ToString();
                lblTongDoanhThuNam.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê năm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
