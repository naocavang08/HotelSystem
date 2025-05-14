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
                    BookingServiceId = i.booking_service_id,
                    CustomerName = i.Booking?.Customer?.name ?? "N/A",
                    RoomNumber = i.Booking?.Room?.room_number ?? "N/A",
                    TotalAmount = i.total_amount,
                    PaymentStatus = i.payment_status,
                    IssueDate = i.payment_date ?? DateTime.MinValue
                }).ToList();
            }

            public InvoiceDTO GetInvoiceById(int id)
            {
                var invoice = _invoiceDAL.GetInvoiceById(id);
                return invoice == null ? null : new InvoiceDTO
                {
                    Id = invoice.invoice_id,
                    BookingId = invoice.booking_id,
                    BookingServiceId=invoice.booking_service_id,
                    TotalAmount = invoice.total_amount,
                    PaymentStatus = invoice.payment_status
                };
            }

            public bool AddInvoice(InvoiceDTO dto)
            {
                var invoice = new Invoice
                {
                    booking_id = dto.BookingId,
                  
                    booking_service_id = dto.BookingServiceId,
                    
                    total_amount = dto.TotalAmount,
                    payment_status = dto.PaymentStatus,
                    payment_date = dto.IssueDate
                };
                return _invoiceDAL.AddInvoice(invoice);
            }
            public bool UpdateInvoice(InvoiceDTO dto)
            {
                var invoice = _invoiceDAL.GetInvoiceById(dto.Id);
                if (invoice == null) return false;

                invoice.booking_id = dto.BookingId;
            invoice.booking_service_id = dto.BookingServiceId;
            invoice.total_amount = dto.TotalAmount;
                invoice.payment_status = dto.PaymentStatus;
            invoice.payment_date = dto.IssueDate;

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

    public class InvoiceDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int BookingServiceId { get; set; }
        public string CustomerName { get; set; }
        public string RoomNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
        public string PaymentStatus { get; set; }
    }

}