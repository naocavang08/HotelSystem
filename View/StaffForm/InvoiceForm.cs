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
using HotelSystem.View.CustomerForm;

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

            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
            editColumn.HeaderText = "Edit";
            editColumn.Text = "Edit";
            editColumn.UseColumnTextForButtonValue = true;
            dgvInvoices.Columns.Add(editColumn);
            
            // Add double-click event handler
            dgvInvoices.CellDoubleClick += DgvInvoices_CellDoubleClick;
            
            // Create context menu
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
            // Check if the click is on the Edit button column
            if (e.ColumnIndex == dgvInvoices.Columns.Count - 1 && e.RowIndex >= 0)
            {
                if (e.RowIndex < invoices.Count)
                {
                    // Open edit dialog or enable editing for this row
                    dgvInvoices.BeginEdit(true);
                }
            }
            // If the click is on any other column, open the invoice details
            else if (e.RowIndex >= 0 && e.RowIndex < invoices.Count)
            {
                int invoiceId = invoices[e.RowIndex].InvoiceId;
                
                // Open the invoice details form
                CustomerForm.Invoice invoiceForm = new CustomerForm.Invoice(invoiceId);
                invoiceForm.ShowDialog();
            }
        }

        private void dgvInvoices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Error in data entry. Please check your input.", "Data Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false;
        }

        private void SaveInvoice(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < invoices.Count)
            {
                DTO_Invoice invoice = invoices[rowIndex];
                
                // Determine if it's a new or existing invoice
                if (invoice.InvoiceId > 0)
                {
                    // Update existing invoice
                    bllInvoice.UpdateInvoice(invoice);
                }
                else
                {
                    // Create new invoice
                    bllInvoice.CreateInvoice(invoice);
                    LoadInvoiceData(); // Reload to get the new ID
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index >= 0 
                && dgvInvoices.CurrentRow.Index < invoices.Count)
            {
                int invoiceId = invoices[dgvInvoices.CurrentRow.Index].InvoiceId;
                
                // Confirm deletion
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn không?", 
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    using (var db = new DBHotelSystem())
                    {
                        try
                        {
                            // Get the invoice with related bookings and services
                            var invoice = db.Invoices
                                .Include("Bookings.Customer")
                                .Include("BookingServices")
                                .FirstOrDefault(i => i.invoice_id == invoiceId);
                                
                            if (invoice != null && invoice.Bookings.Any())
                            {
                                // Get the customer ID from the first booking
                                int customerId = invoice.Bookings.First().customer_id;
                                
                                // Get all completed bookings for this customer
                                var completedBookings = db.Bookings
                                    .Where(b => b.customer_id == customerId && b.status == "Checked Out")
                                    .ToList();
                                    
                                // Get all completed booking services for this customer
                                var completedBookingServices = db.BookingServices
                                    .Where(bs => bs.customer_id == customerId && bs.status == "Completed")
                                    .ToList();
                                
                                // First remove the invoice
                                db.Invoices.Remove(invoice);
                                db.SaveChanges();
                                
                                // Delete all completed bookings for this customer
                                foreach (var booking in completedBookings)
                                {
                                    db.Bookings.Remove(booking);
                                }
                                
                                // Delete all completed booking services for this customer
                                foreach (var bookingService in completedBookingServices)
                                {
                                    db.BookingServices.Remove(bookingService);
                                }
                                
                                db.SaveChanges();
                                
                                MessageBox.Show("Hóa đơn đã được xóa thành công!", 
                                    "Xóa thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Reload data
                                LoadInvoiceData();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hóa đơn hoặc không có thông tin đặt phòng!", 
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa.", "Chưa chọn", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                invoices = bllInvoice.SearchInvoices(searchTerm);
                dgvInvoices.DataSource = invoices;
                dgvInvoices.Refresh();
            }
            else
            {
                LoadInvoiceData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DTO_Invoice newInvoice = new DTO_Invoice
            {
                TotalAmount = 0,
                PaymentStatus = "Pending",
                PaymentDate = null,
                BookingRoomIds = new List<int>(),
                BookingServiceIds = new List<int>()
            };
            
            invoices.Add(newInvoice);
            dgvInvoices.DataSource = null;
            dgvInvoices.DataSource = invoices;
            dgvInvoices.Refresh();
            
            // Focus on the new row for editing
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
            txtSearch.Clear();
            LoadInvoiceData();
        }

        private void DgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < invoices.Count)
            {
                int invoiceId = invoices[e.RowIndex].InvoiceId;
                
                // Open the invoice details form
                CustomerForm.Invoice invoiceForm = new CustomerForm.Invoice(invoiceId);
                invoiceForm.ShowDialog();
            }
        }

        private void ViewMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index >= 0 
                && dgvInvoices.CurrentRow.Index < invoices.Count)
            {
                int invoiceId = invoices[dgvInvoices.CurrentRow.Index].InvoiceId;
                
                // Open the invoice details form
                CustomerForm.Invoice invoiceForm = new CustomerForm.Invoice(invoiceId);
                invoiceForm.ShowDialog();
            }
        }

        private void InvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}