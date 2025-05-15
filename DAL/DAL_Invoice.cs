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
            try
            {
                var invoice = db.Invoices.Find(invoiceId);
                if (invoice == null)
                    return false;

                db.Invoices.Remove(invoice);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Invoice> SearchInvoices(string searchTerm)
        {
            return db.Invoices.Include("Bookings").Include("BookingServices")
                .Where(i => i.payment_status.Contains(searchTerm) || 
                           i.invoice_id.ToString().Contains(searchTerm))
                .ToList();
        }
    }
}
