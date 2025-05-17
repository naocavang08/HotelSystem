using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelSystem.Model;

namespace HotelSystem.DAL
{
    public class DAL_Invoice
    {
        private DBHotelSystem db = new DBHotelSystem();

        public List<Invoice> GetAllInvoices()
        {
            return db.Invoices.Include("Bookings").Include("BookingServices").ToList();
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return db.Invoices.Include("Bookings").Include("BookingServices")
                .FirstOrDefault(i => i.invoice_id == invoiceId);
        }

        public bool CreateInvoice(Invoice invoice)
        {
            try
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            try
            {
                var existingInvoice = db.Invoices.Find(invoice.invoice_id);
                if (existingInvoice == null)
                    return false;

                existingInvoice.total_amount = invoice.total_amount;
                existingInvoice.payment_status = invoice.payment_status;
                existingInvoice.payment_date = invoice.payment_date;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteInvoice(int invoiceId)
        {
            using (var db = new DBHotelSystem())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Kiểm tra xem invoice có tồn tại không
                        var invoice = db.Invoices
                            .Include("Bookings")
                            .Include("BookingServices")
                            .FirstOrDefault(i => i.invoice_id == invoiceId);
                        
                        if (invoice == null)
                        {
                            Console.WriteLine($"Invoice with ID {invoiceId} not found for deletion.");
                            return false;
                        }

                        // Xóa tham chiếu đến invoice trong các bảng liên kết nếu cần
                        if (invoice.Bookings != null && invoice.Bookings.Any())
                        {
                            foreach (var booking in invoice.Bookings.ToList())
                            {
                                invoice.Bookings.Remove(booking);
                            }
                        }

                        if (invoice.BookingServices != null && invoice.BookingServices.Any())
                        {
                            foreach (var service in invoice.BookingServices.ToList())
                            {
                                invoice.BookingServices.Remove(service);
                            }
                        }

                        db.SaveChanges();

                        // Sau khi đã xóa các liên kết, xóa hóa đơn
                        db.Invoices.Remove(invoice);
                        db.SaveChanges();
                        
                        transaction.Commit();
                        Console.WriteLine($"Invoice with ID {invoiceId} successfully deleted.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting invoice {invoiceId}: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<Invoice> SearchInvoices(string searchTerm)
        {
            return db.Invoices.Include("Bookings").Include("BookingServices")
                .Where(i => i.payment_status.Contains(searchTerm) || 
                           i.invoice_id.ToString().Contains(searchTerm))
                .ToList();
        }
        
        public int GetNextInvoiceId()
        {
            using (var newContext = new DBHotelSystem()) // Sử dụng context mới để đảm bảo dữ liệu mới nhất
            {
                if (newContext.Invoices.Any())
                {
                    return newContext.Invoices.Max(i => i.invoice_id) + 1;
                }
                else
                {
                    return 1; // Nếu không có hóa đơn nào, bắt đầu từ 1
                }
            }
        }
    }
}
