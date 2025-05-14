using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.Model;

namespace HotelSystem.DAL
{
    public class InvoiceDAL
    {
        private readonly DBHotelSystem _db;

        public InvoiceDAL()
        {
            _db = new DBHotelSystem();
            _db.Configuration.LazyLoadingEnabled = false; // Tắt lazy loading để tránh lỗi
        }

        public List<Invoice> GetAllInvoices()
        {
            return _db.Invoices
                .Include(i => i.Booking)
                .Include(i => i.BookingService)
                .Include(i => i.Booking.Customer)
                .Include(i => i.Booking.Room)
                .AsNoTracking() // Tăng performance
                .ToList();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _db.Invoices
                .Include(i => i.Booking)
                .AsNoTracking()
                .FirstOrDefault(i => i.invoice_id == id);
        }

        public bool AddInvoice(Invoice invoice)
        {
            try
            {
                _db.Invoices.Add(invoice);
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddInvoice: {ex.Message}");
                return false;
            }
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            try
            {
                _db.Entry(invoice).State = EntityState.Modified;
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateInvoice: {ex.Message}");
                return false;
            }
        }

        public bool DeleteInvoice(int id)
        {
            try
            {
                var invoice = _db.Invoices.Find(id);
                if (invoice != null)
                {
                    _db.Invoices.Remove(invoice);
                    return _db.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteInvoice: {ex.Message}");
                return false;
            }
        }
    }
}