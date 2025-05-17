using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;

namespace HotelSystem.BLL
{
    public class BLL_Invoice
    {
        private DAL_Invoice dalInvoice = new DAL_Invoice();

        public List<DTO_Invoice> GetAllInvoices()
        {
            var invoices = dalInvoice.GetAllInvoices();
            if (invoices == null || !invoices.Any())
            {
                return new List<DTO_Invoice>();
            }
            return ConvertToDTO(invoices);
        }

        public DTO_Invoice GetInvoiceById(int invoiceId)
        {
            // Sử dụng một DbContext mới để đảm bảo lấy dữ liệu mới nhất
            using (var newContext = new DBHotelSystem())
            {
                try
                {
                    var invoice = newContext.Invoices
                        .Include("Bookings")
                        .Include("BookingServices")
                        .FirstOrDefault(i => i.invoice_id == invoiceId);
                        
                    if (invoice == null)
                    {
                        Console.WriteLine($"Invoice with ID {invoiceId} not found.");
                        return null;
                    }
                    
                    return ConvertToDTO(invoice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving invoice {invoiceId}: {ex.Message}");
                    return null;
                }
            }
        }

        public bool CreateInvoice(DTO_Invoice dtoInvoice)
        {
            var invoice = new Invoice
            {
                total_amount = dtoInvoice.TotalAmount,
                payment_status = dtoInvoice.PaymentStatus,
                payment_date = dtoInvoice.PaymentDate
            };
            
            return dalInvoice.CreateInvoice(invoice);
        }

        public bool UpdateInvoice(DTO_Invoice dtoInvoice)
        {
            var invoice = dalInvoice.GetInvoiceById(dtoInvoice.InvoiceId);
            if (invoice == null)
            {
                return false;
            }

            invoice.total_amount = dtoInvoice.TotalAmount;
            invoice.payment_status = dtoInvoice.PaymentStatus;
            invoice.payment_date = dtoInvoice.PaymentDate;

            return dalInvoice.UpdateInvoice(invoice);
        }

        public bool DeleteInvoice(int invoiceId)
        {
            return dalInvoice.DeleteInvoice(invoiceId);
        }

        public List<DTO_Invoice> SearchInvoices(string searchTerm)
        {
            var invoices = dalInvoice.SearchInvoices(searchTerm);
            if (invoices == null || !invoices.Any())
            {
                return new List<DTO_Invoice>();
            }
            return ConvertToDTO(invoices);
        }
        
        public int GetNextInvoiceId()
        {
            return dalInvoice.GetNextInvoiceId();
        }

        private List<DTO_Invoice> ConvertToDTO(List<Invoice> invoices)
        {
            if (invoices == null)
                return new List<DTO_Invoice>();
                
            return invoices.Select(i => ConvertToDTO(i)).ToList();
        }

        private DTO_Invoice ConvertToDTO(Invoice invoice)
        {
            if (invoice == null)
                return null;
                
            return new DTO_Invoice
            {
                InvoiceId = invoice.invoice_id,
                TotalAmount = invoice.total_amount,
                PaymentStatus = invoice.payment_status,
                PaymentDate = invoice.payment_date,
                BookingRoomIds = invoice.Bookings?.Select(b => b.booking_id).ToList() ?? new List<int>(),
                BookingServiceIds = invoice.BookingServices?.Select(bs => bs.booking_service_id).ToList() ?? new List<int>()
            };
        }
    }
}
