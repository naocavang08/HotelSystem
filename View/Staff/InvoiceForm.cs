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
using HotelSystem.DTO;

namespace HotelSystem.View.Staff
{
    public partial class InvoiceForm : Form
    {
        private readonly InvoiceBLL _invoiceBLL = new InvoiceBLL();
        private List<BLL.InvoiceDTO> _invoiceList;

        public InvoiceForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            dgvInvoices.DataError += dgvInvoices_DataError; // Bắt lỗi hiển thị dữ liệu sai
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

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Mã HĐ",
                Name = "Id",
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingId",
                HeaderText = "Mã Đặt phòng",
                Name = "BookingId"
            });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingServiceId", 
                HeaderText = "Mã Đặt Dịch vụ",
                Name = "BookingServiceId"
            });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Tổng tiền",
                Name = "TotalAmount",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IssueDate",
                HeaderText = "Ngày tạo",
                Name = "IssueDate"
            });

            var statusCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "PaymentStatus",
                HeaderText = "Trạng thái",
                Name = "PaymentStatus",
                DataSource = new List<string> { "Paid", "Unpaid", "Cancelled" },
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
            };
            dgvInvoices.Columns.Add(statusCol);

            var saveButton = new DataGridViewButtonColumn
            {
                Text = "Lưu",
                UseColumnTextForButtonValue = true,
                HeaderText = "Thao tác",
                Name = "SaveButton"
            };
            dgvInvoices.Columns.Add(saveButton);
        }

        private void dgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvInvoices.Columns["SaveButton"].Index)
            {
                SaveInvoice(e.RowIndex);
            }
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvInvoices.Columns["SaveButton"].Index && e.RowIndex >= 0)
            {
                SaveInvoice(e.RowIndex);
            }
        }

        private void dgvInvoices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Giá trị không hợp lệ trong ô. Vui lòng kiểm tra lại.", "Lỗi dữ liệu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }

        private void SaveInvoice(int rowIndex)
        {
            try
            {
                var row = dgvInvoices.Rows[rowIndex];
                var invoiceDTO = _invoiceList[rowIndex];

                invoiceDTO.BookingId = Convert.ToInt32(row.Cells["BookingId"].Value);
                invoiceDTO.BookingServiceId = Convert.ToInt32(row.Cells["BookingServiceId"].Value);


                invoiceDTO.TotalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                invoiceDTO.IssueDate = Convert.ToDateTime(row.Cells["IssueDate"].Value);
                invoiceDTO.PaymentStatus = row.Cells["PaymentStatus"].Value?.ToString();

                bool result;

                if (invoiceDTO.Id == 0)
                {
                    result = _invoiceBLL.AddInvoice(invoiceDTO); // cần viết hàm AddInvoice
                    if (result)
                        MessageBox.Show("Thêm hóa đơn mới thành công!", "Thông báo");
                }
                else
                {
                    result = _invoiceBLL.UpdateInvoice(invoiceDTO);
                    if (result)
                        MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo");
                }

                if (result)
                    LoadInvoiceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (_invoiceList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newInvoice = new BLL.InvoiceDTO
            {
                Id = 0, // giả định Id = 0 là hóa đơn mới
                BookingId = 0,
                BookingServiceId = 0,
                TotalAmount = 0,
                IssueDate = DateTime.Now,
                PaymentStatus = "Unpaid" // hoặc để null nếu muốn chọn
            };

            _invoiceList.Add(newInvoice);
            dgvInvoices.DataSource = null;
            dgvInvoices.DataSource = _invoiceList;
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInvoiceData();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lại dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}