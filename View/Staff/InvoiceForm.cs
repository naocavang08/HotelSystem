using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.BLL;
using System.Windows.Forms;
using HotelSystem.DTO;

namespace HotelSystem.View.Staff
{
    public partial class InvoiceForm : Form
    {
        private readonly InvoiceBLL _invoiceBLL = new InvoiceBLL();
        private List<InvoiceDTO> _invoiceList;

        public InvoiceForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadInvoiceData();
        }

        private void LoadInvoiceData()
        {
            try
            {
                _invoiceList = _invoiceBLL.GetAllInvoices();
                dgvInvoices.DataSource = _invoiceList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hóa đơn: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.AllowUserToAddRows = false;
            dgvInvoices.AllowUserToDeleteRows = false;
            dgvInvoices.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvoices.Columns.Clear();

            // Thêm các cột
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Mã HĐ",
                ReadOnly = true // Không cho sửa ID
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingId",
                HeaderText = "Mã Đặt phòng"
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Tổng tiền",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IssueDate",
                HeaderText = "Ngày tạo"
            });

            // Sử dụng ComboBox cho trạng thái thanh toán
            var statusCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "PaymentStatus",
                HeaderText = "Trạng thái",
                DataSource = new[] { "Paid", "Unpaid", "Cancelled" }
            };
            dgvInvoices.Columns.Add(statusCol);

            // Thêm nút Lưu
            var saveButton = new DataGridViewButtonColumn
            {
                Text = "Lưu",
                UseColumnTextForButtonValue = true,
                HeaderText = "Thao tác"
            };
            dgvInvoices.Columns.Add(saveButton);
        }

        private void dgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Tự động lưu khi kết thúc chỉnh sửa
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvInvoices.Columns["Lưu"].Index)
            {
                SaveInvoice(e.RowIndex);
            }
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi click nút Lưu
            if (e.ColumnIndex == dgvInvoices.Columns["Lưu"].Index && e.RowIndex >= 0)
            {
                SaveInvoice(e.RowIndex);
            }
        }

        private void SaveInvoice(int rowIndex)
        {
            try
            {
                var row = dgvInvoices.Rows[rowIndex];
                var invoiceDTO = _invoiceList[rowIndex];

                // Cập nhật giá trị từ DataGridView vào DTO
                invoiceDTO.BookingId = Convert.ToInt32(row.Cells["BookingId"].Value);
                invoiceDTO.TotalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                invoiceDTO.IssueDate = Convert.ToDateTime(row.Cells["IssueDate"].Value);
                invoiceDTO.PaymentStatus = row.Cells["PaymentStatus"].Value.ToString();

                if (_invoiceBLL.UpdateInvoice(invoiceDTO))
                {
                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo");
                    LoadInvoiceData(); // Refresh dữ liệu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hóa đơn: {ex.Message}", "Lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa", "Thông báo");
                return;
            }

            var selectedId = (int)dgvInvoices.SelectedRows[0].Cells["Id"].Value;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (_invoiceBLL.DeleteInvoice(selectedId))
                    {
                        MessageBox.Show("Xóa hóa đơn thành công", "Thông báo");
                        LoadInvoiceData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtSearch.Text.Trim();
                _invoiceList = string.IsNullOrEmpty(keyword)
                    ? _invoiceBLL.GetAllInvoices()
                    : _invoiceBLL.SearchInvoices(keyword);
                dgvInvoices.DataSource = _invoiceList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi");
            }
        }
    }
}