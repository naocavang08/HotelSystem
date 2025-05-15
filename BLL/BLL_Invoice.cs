using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var invoice = dalInvoice.GetInvoiceById(invoiceId);
            if (invoice == null)
            {
                return null;
            }
            return ConvertToDTO(invoice);
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

        private List<DTO_Invoice> ConvertToDTO(List<Invoice> invoices)
        {
            return invoices.Select(i => ConvertToDTO(i)).ToList();
        }

        private DTO_Invoice ConvertToDTO(Invoice invoice)
        {
            return new DTO_Invoice
            {
                InvoiceId = invoice.invoice_id,
                TotalAmount = invoice.total_amount,
                PaymentStatus = invoice.payment_status,
                PaymentDate = invoice.payment_date,
                BookingRoomIds = invoice.Bookings.Select(b => b.booking_id).ToList(),
                BookingServiceIds = invoice.BookingServices.Select(bs => bs.booking_service_id).ToList()
            };
        }
    }
}
