using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;

namespace HotelSystem.BLL
{
    public class BLL_BookingService
    {
        private DAL_BookingService dalBookingService = new DAL_BookingService();

        // Thêm dịch vụ đặt phòng
        public void AddBookingService(DTO_BookingService dtoBookingService)
        {
            var bookingService = new BookingService
            {
                customer_id = dtoBookingService.CustomerId,
                service_id = dtoBookingService.Service_id,
                quantity = dtoBookingService.Quantity,
                service_date = dtoBookingService.Service_date,
                total_price = dtoBookingService.TotalPrice,
                status = dtoBookingService.Status ?? "Booked" // Sử dụng giá trị từ DTO hoặc mặc định
            };

            dalBookingService.AddBookingService(bookingService);
        }

        // Lấy danh sách tất cả dịch vụ đã đặt
        public List<DTO_BookingService> GetBookingServices()
        {
            var bookingServices = dalBookingService.GetBookingServices();
            var dtoBookingServices = new List<DTO_BookingService>();

            foreach (var bs in bookingServices)
            {
                dtoBookingServices.Add(new DTO_BookingService
                {
                    Booking_service_id = bs.booking_service_id,
                    CustomerId = bs.customer_id,
                    Service_id = bs.service_id,
                    Quantity = bs.quantity,
                    Service_date = bs.service_date,
                    TotalPrice = bs.total_price,
                    Status = bs.status
                });
            }

            return dtoBookingServices;
        }

        // Lấy danh sách dịch vụ đã đặt theo ServiceId
        public List<DTO_BookingService> GetBookingServicesByServiceId(int serviceId)
        {
            var bookingServices = dalBookingService.GetBookingServicesByServiceId(serviceId);
            var dtoBookingServices = new List<DTO_BookingService>();

            foreach (var bs in bookingServices)
            {
                dtoBookingServices.Add(new DTO_BookingService
                {
                    Booking_service_id = bs.booking_service_id,
                    CustomerId = bs.customer_id,
                    Service_id = bs.service_id,
                    Quantity = bs.quantity,
                    Service_date = bs.service_date,
                    TotalPrice = bs.total_price,
                    Status = bs.status
                });
            }

            return dtoBookingServices;
        }

        // Lấy danh sách dịch vụ đã đặt theo CustomerId
        public List<DTO_BookingService> GetBookingServicesByCustomerId(int customerId)
        {
            var bookingServices = dalBookingService.GetBookingServicesByCustomerId(customerId);
            var dtoBookingServices = new List<DTO_BookingService>();
            foreach (var bs in bookingServices)
            {
                dtoBookingServices.Add(new DTO_BookingService
                {
                    Booking_service_id = bs.booking_service_id,
                    CustomerId = bs.customer_id,
                    Service_id = bs.service_id,
                    Quantity = bs.quantity,
                    Service_date = bs.service_date,
                    TotalPrice = bs.total_price,
                    Status = bs.status
                });
            }
            return dtoBookingServices;
        }

        // Xóa dịch vụ đã đặt
        public void DeleteBookingService(int bookingServiceId)
        {
            dalBookingService.DeleteBookingService(bookingServiceId);
        }

        // Cập nhật dịch vụ đã đặt
        public void UpdateBookingService(DTO_BookingService dtoBookingService)
        {
            var bookingService = new BookingService
            {
                booking_service_id = dtoBookingService.Booking_service_id,
                customer_id = dtoBookingService.CustomerId,
                service_id = dtoBookingService.Service_id,
                quantity = dtoBookingService.Quantity,
                service_date = dtoBookingService.Service_date,
                total_price = dtoBookingService.TotalPrice,
                status = dtoBookingService.Status ?? "Booked" // Sử dụng giá trị từ DTO hoặc mặc định
            };

            dalBookingService.UpdateBookingService(bookingService);
        }
        
        // Cập nhật trạng thái dịch vụ
        public void UpdateBookingServiceStatus(int bookingServiceId, string status)
        {
            dalBookingService.UpdateBookingServiceStatus(bookingServiceId, status);
        }
    }
}