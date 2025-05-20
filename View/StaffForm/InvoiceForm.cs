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
using HotelSystem.Model;
using HotelSystem.View.StaffForm;

namespace HotelSystem.View.StaffForm
{
    public partial class InvoiceForm : Form
    {
        private BLL_Invoice bllInvoice = new BLL_Invoice();
        private List<DTO_Invoice> invoices = new List<DTO_Invoice>();

        public InvoiceForm()
        {
            InitializeComponent();
        }

        private void LoadInvoiceData()
        {
            invoices = bllInvoice.GetAllInvoices();
            dgvInvoices.DataSource = invoices;
            dgvInvoices.Refresh();
        }

        private void ConfigureDataGridView()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.Columns.Clear();

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "InvoiceId";
            idColumn.HeaderText = "Invoice ID";
            dgvInvoices.Columns.Add(idColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.DataPropertyName = "TotalAmount";
            amountColumn.HeaderText = "Total Amount";
            dgvInvoices.Columns.Add(amountColumn);

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "PaymentStatus";
            statusColumn.HeaderText = "Payment Status";
            dgvInvoices.Columns.Add(statusColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DataPropertyName = "PaymentDate";
            dateColumn.HeaderText = "Payment Date";
            dgvInvoices.Columns.Add(dateColumn);

            DataGridViewButtonColumn detailColumn = new DataGridViewButtonColumn();
            detailColumn.HeaderText = "Detail";
            detailColumn.Text = "Detail";
            detailColumn.UseColumnTextForButtonValue = true;
            dgvInvoices.Columns.Add(detailColumn);
            
            // Thêm sự kiện xử lý khi double-click vào ô
            dgvInvoices.CellDoubleClick += DgvInvoices_CellDoubleClick;
            
            // Tạo menu ngữ cảnh
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem viewMenuItem = new ToolStripMenuItem("Xem chi tiết hóa đơn");
            viewMenuItem.Click += ViewMenuItem_Click;
            contextMenu.Items.Add(viewMenuItem);
            
            dgvInvoices.ContextMenuStrip = contextMenu;
        }

        private void dgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < invoices.Count)
            {
                SaveInvoice(e.RowIndex);
            }
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã nhấp vào cột nút Chỉnh sửa chưa
            if (e.ColumnIndex == dgvInvoices.Columns.Count - 1 && e.RowIndex >= 0)
            {
                if (e.RowIndex < invoices.Count)
                {
                    // Mở hộp thoại chỉnh sửa hoặc cho phép chỉnh sửa cho hàng này
                    dgvInvoices.BeginEdit(true);
                }
            }
            // Nếu nhấp vào bất kỳ cột nào khác, mở chi tiết hóa đơn
            else if (e.RowIndex >= 0 && e.RowIndex < invoices.Count)
            {
                int invoiceId = invoices[e.RowIndex].InvoiceId;
                
                // Mở form chi tiết hóa đơn với form này là người gọi
                Invoice invoiceForm = new Invoice(invoiceId, this);
                invoiceForm.ShowDialog();
            }
        }

        private void dgvInvoices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Lỗi dữ liệu. Hãy kiểm tra lại đầu vào.", "Lỗi dữ liệu", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false;
        }

        private void SaveInvoice(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < invoices.Count)
            {
                DTO_Invoice invoice = invoices[rowIndex];
                
                // Xác định xem đây là hóa đơn mới hay hiện có
                if (invoice.InvoiceId > 0)
                {
                    // Cập nhật hóa đơn hiện có
                    bllInvoice.UpdateInvoice(invoice);
                }
                else
                {
                    // Tạo hóa đơn mới
                    bllInvoice.CreateInvoice(invoice);
                    LoadInvoiceData(); // Làm mới danh sách để lấy ID mới
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index >= 0 
                && dgvInvoices.CurrentRow.Index < invoices.Count)
            {
                int invoiceId = invoices[dgvInvoices.CurrentRow.Index].InvoiceId;
                
                // Xác nhận xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn không?", 
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Kiểm tra sự tồn tại của invoice trước
                        var bllInvoice = new BLL_Invoice();
                        var dtoInvoice = bllInvoice.GetInvoiceById(invoiceId);
                        
                        if (dtoInvoice == null)
                        {
                            MessageBox.Show($"Không tìm thấy hóa đơn có ID: {invoiceId}. Hóa đơn có thể đã bị xóa.", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoadInvoiceData(); // Làm mới danh sách để đồng bộ
                            return;
                        }
                        
                        using (var db = new DBHotelSystem())
                        {
                            // Thêm transaction để đảm bảo tính nhất quán
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    // Lấy hóa đơn với các đặt phòng và dịch vụ liên quan
                                    var invoice = db.Invoices
                                        .Include("Bookings.Customer")
                                        .Include("BookingServices")
                                        .FirstOrDefault(i => i.invoice_id == invoiceId);
                                        
                                    if (invoice == null)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Không tìm thấy hóa đơn có ID: {invoiceId} trong database.", 
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        LoadInvoiceData();
                                        return;
                                    }
                                    
                                    if (!invoice.Bookings.Any())
                                    {
                                        // Nếu không có bookings liên kết, chỉ xóa invoice
                                        db.Invoices.Remove(invoice);
                                        db.SaveChanges();
                                        transaction.Commit();
                                        
                                        MessageBox.Show("Hóa đơn đã được xóa thành công!", 
                                            "Xóa thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        LoadInvoiceData();
                                        return;
                                    }
                                        
                                    // Lấy ID khách hàng từ đặt phòng đầu tiên
                                    int customerId = invoice.Bookings.First().customer_id;
                                    
                                    // Lấy tất cả đặt phòng đã hoàn thành của khách hàng này
                                    var completedBookings = db.Bookings
                                        .Where(b => b.customer_id == customerId && b.status == "Checked Out")
                                        .ToList();
                                        
                                    // Lấy tất cả dịch vụ đặt phòng đã hoàn thành của khách hàng này
                                    var completedBookingServices = db.BookingServices
                                        .Where(bs => bs.customer_id == customerId && bs.status == "Completed")
                                        .ToList();
                                    
                                    // Đầu tiên xóa hóa đơn
                                    db.Invoices.Remove(invoice);
                                    db.SaveChanges();
                                    
                                    // Xóa tất cả đặt phòng đã hoàn thành của khách hàng này
                                    foreach (var booking in completedBookings)
                                    {
                                        db.Bookings.Remove(booking);
                                    }
                                    
                                    // Xóa tất cả dịch vụ đặt phòng đã hoàn thành của khách hàng này
                                    foreach (var bookingService in completedBookingServices)
                                    {
                                        db.BookingServices.Remove(bookingService);
                                    }
                                    
                                    db.SaveChanges();
                                    transaction.Commit();
                                    
                                    MessageBox.Show("Hóa đơn đã được xóa thành công!", 
                                        "Xóa thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    // Làm mới dữ liệu
                                    LoadInvoiceData();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show($"Lỗi trong transaction khi xóa hóa đơn: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", 
                                        "Lỗi transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                List<DTO_Invoice> searchResults = bllInvoice.SearchInvoices(searchText);
                dgvInvoices.DataSource = searchResults;
                dgvInvoices.Refresh();
            }
            else
            {
                LoadInvoiceData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BLL_Invoice bllInvoice = new BLL_Invoice();
            var nextInvoiceId = bllInvoice.GetNextInvoiceId();
            DTO_Invoice newInvoice = new DTO_Invoice
            {
                InvoiceId = nextInvoiceId,
                TotalAmount = 0,
                PaymentStatus = "Chưa thanh toán",
                PaymentDate = null,
                BookingRoomIds = new List<int>(),
                BookingServiceIds = new List<int>()
            };
            invoices.Add(newInvoice);
            dgvInvoices.DataSource = null;
            dgvInvoices.DataSource = invoices;
            dgvInvoices.Refresh();
            dgvInvoices.CurrentCell = dgvInvoices.Rows[dgvInvoices.Rows.Count - 1].Cells[0];
            dgvInvoices.BeginEdit(true);
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadInvoiceData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadInvoiceData();
        }

        private void DgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < invoices.Count)
            {
                int invoiceId = invoices[e.RowIndex].InvoiceId;
                
                // Mở form chi tiết hóa đơn với form này là người gọi
                Invoice invoiceForm = new Invoice(invoiceId, this);
                invoiceForm.ShowDialog();
            }
        }

        private void ViewMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index >= 0 
                && dgvInvoices.CurrentRow.Index < invoices.Count)
            {
                int invoiceId = invoices[dgvInvoices.CurrentRow.Index].InvoiceId;
                
                // Mở form chi tiết hóa đơn với form này là người gọi
                Invoice invoiceForm = new Invoice(invoiceId, this);
                invoiceForm.ShowDialog();
            }
        }

        private void InvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StaffForm customerForm = new StaffForm();
            customerForm.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            StaffForm staff = new StaffForm();
            staff.Show();
        }
    }
}