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
    public class InvoiceBLL
    {
        private readonly InvoiceDAL _invoiceDAL = new InvoiceDAL();

        public List<InvoiceDTO> GetAllInvoices()
        {
            return _invoiceDAL.GetAllInvoices().Select(i => new InvoiceDTO
            {
                Id = i.invoice_id,
                BookingId = i.booking_id,
                CustomerName = i.Booking.Customer.name,
                RoomNumber = i.Booking.Room.room_number,
                TotalAmount = i.total_amount,
                PaymentStatus = i.payment_status
            }).ToList();
        }

        public InvoiceDTO GetInvoiceById(int id)
        {
            var invoice = _invoiceDAL.GetInvoiceById(id);
            return invoice == null ? null : new InvoiceDTO
            {
                Id = invoice.invoice_id,
                BookingId = invoice.booking_id,
                TotalAmount = invoice.total_amount,
                PaymentStatus = invoice.payment_status
            };
        }

        public bool AddInvoice(InvoiceDTO dto)
        {
            return _invoiceDAL.AddInvoice(new Invoice
            {
                booking_id = dto.BookingId,
                total_amount = dto.TotalAmount,
                payment_status = dto.PaymentStatus
            });
        }

        public bool UpdateInvoice(InvoiceDTO dto)
        {
            var invoice = _invoiceDAL.GetInvoiceById(dto.Id);
            if (invoice == null) return false;

            invoice.booking_id = dto.BookingId;
            invoice.total_amount = dto.TotalAmount;
            invoice.payment_status = dto.PaymentStatus;

            return _invoiceDAL.UpdateInvoice(invoice);
        }

        public bool DeleteInvoice(int id) => _invoiceDAL.DeleteInvoice(id);

        public List<InvoiceDTO> SearchInvoices(string keyword)
        {
            return GetAllInvoices().Where(i =>
                i.CustomerName.Contains(keyword) ||
                i.RoomNumber.Contains(keyword) ||
                i.PaymentStatus.Contains(keyword) ||
                i.Id.ToString().Contains(keyword)
            ).ToList();
        }
    }
}