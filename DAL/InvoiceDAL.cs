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
        }

        public List<Invoice> GetAllInvoices()
        {
            return _db.Invoices
                .Include(i => i.Booking)
                .Include(i => i.Booking.Customer)
                .Include(i => i.Booking.Room)
                .ToList();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _db.Invoices
                .Include(i => i.Booking)
                .FirstOrDefault(i => i.invoice_id == id);
        }

        public bool AddInvoice(Invoice invoice)
        {
            try
            {
                _db.Invoices.Add(invoice);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            try
            {
                _db.Entry(invoice).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch
            {
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
                    _db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}