using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
            cbbLoaiThongKe.SelectedIndex = 0; // Mặc định chọn thống kê theo tháng
        }

        // Tải dữ liệu vào các ComboBox
        private void LoadComboBoxes()
        {
            for (int i = 1; i <= 12; i++) cbbThang.Items.Add(i);
            cbbThang.SelectedIndex = DateTime.Now.Month - 1;

            for (int i = 1; i <= 4; i++) cbbQuy.Items.Add($"Quý {i}");
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            cbbQuy.SelectedIndex = currentQuarter - 1;

            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++) cbbNam.Items.Add(currentYear - i);
            cbbNam.SelectedIndex = 0;

            cbbLoaiThongKe.Items.Add("Theo tháng");
            cbbLoaiThongKe.Items.Add("Theo quý");
            cbbLoaiThongKe.Items.Add("Theo năm");
        }

        // Xử lý khi thay đổi loại thống kê
        private void cbbLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbbLoaiThongKe.SelectedIndex)
            {
                case 0:
                    SetPanelState(panelThang, true);
                    SetPanelState(panelQuy, false);
                    SetPanelState(panelNam, false);
                    UpdateThongKeThang();
                    break;
                case 1:
                    SetPanelState(panelThang, false);
                    SetPanelState(panelQuy, true);
                    SetPanelState(panelNam, false);
                    UpdateThongKeQuy();
                    break;
                case 2:
                    SetPanelState(panelThang, false);
                    SetPanelState(panelQuy, false);
                    SetPanelState(panelNam, true);
                    UpdateThongKeNam();
                    break;
            }
        }

        // Bật hoặc tắt trạng thái của các panel
        private void SetPanelState(Panel panel, bool enabled)
        {
            panel.Enabled = enabled;
            panel.BackColor = enabled ? SystemColors.Control : Color.FromArgb(173, 194, 236);
            foreach (Control ctrl in panel.Controls) ctrl.Enabled = enabled;
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

        // Cập nhật thống kê theo tháng
        private void UpdateThongKeThang()
        {
            try
            {
                int selectedMonth = cbbThang.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                //truy vấn bằng cách sử dụng các hàm như Where, ToList, Sum
                var monthlyBookings = db.Bookings
                    .Where(b => b.check_in.Month == selectedMonth && b.check_in.Year == currentYear)
                    .ToList();

                var monthlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                           i.payment_date.Value.Month == selectedMonth &&
                           i.payment_date.Value.Year == currentYear)
                    .ToList();

                int totalBookings = monthlyBookings.Count;
                decimal totalRevenue = monthlyInvoices.Sum(i => i.total_amount);

                lblTongHoaDonThang.Text = totalBookings.ToString();
                lblTongDoanhThuThang.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ"; // Hiển thị định dạng tiền Việt
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê theo tháng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật thống kê theo quý
        private void UpdateThongKeQuy()
        {
            try
            {
                int selectedQuarter = cbbQuy.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                int startMonth = (selectedQuarter - 1) * 3 + 1;
                int endMonth = startMonth + 2;

                var quarterlyBookings = db.Bookings
                    .Where(b => b.check_in.Month >= startMonth &&
                               b.check_in.Month <= endMonth &&
                               b.check_in.Year == currentYear)
                    .ToList();

                var quarterlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                               i.payment_date.Value.Month >= startMonth &&
                               i.payment_date.Value.Month <= endMonth &&
                               i.payment_date.Value.Year == currentYear)
                    .ToList();

                int totalBookings = quarterlyBookings.Count;
                decimal totalRevenue = quarterlyInvoices.Sum(i => i.total_amount);

                lblTongHoaDonQuy.Text = totalBookings.ToString();
                lblTongDoanhThuQuy.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê theo quý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật thống kê theo năm
        private void UpdateThongKeNam()
        {
            try
            {
                int selectedYear = Convert.ToInt32(cbbNam.SelectedItem);

                var yearlyBookings = db.Bookings
                    .Where(b => b.check_in.Year == selectedYear)
                    .ToList();

                var yearlyInvoices = db.Invoices
                    .Where(i => i.payment_date.HasValue &&
                               i.payment_date.Value.Year == selectedYear)
                    .ToList();

                int totalBookings = yearlyBookings.Count;
                decimal totalRevenue = yearlyInvoices.Sum(i => i.total_amount);

                lblTongHoaDonNam.Text = totalBookings.ToString();
                lblTongDoanhThuNam.Text = string.Format("{0:#,##0}", totalRevenue) + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê theo năm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
