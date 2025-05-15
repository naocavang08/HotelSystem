using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;

namespace HotelSystem.BLL
{
    public class BLL_TTKH
    {
        private DAL_TTKH dalTTKH = new DAL_TTKH();
        // Lấy danh sách tất cả khách hàng
        public List<DTO_Customer> GetAllCustomer()
        {
            var customers = dalTTKH.GetAllCustomers();
            var dtoCustomers = new List<DTO_Customer>();
            foreach (var customer in customers)
            {
                dtoCustomers.Add(new DTO_Customer
                {
                    CustomerId = customer.customer_id,
                    Name = customer.name,
                    Phone = customer.phone,
                    CCCD = customer.cccd,
                    Gender = customer.gender,
                    UserId = customer.id
                });
            }
            return dtoCustomers;
        }

        // Lấy thông tin khách hàng theo UserId
        public DTO_Customer GetCustomerByUserId(int userId)
        {
            var customer = dalTTKH.GetCustomerByUserId(userId);
            if (customer == null) return null;

            return new DTO_Customer
            {
                CustomerId = customer.customer_id,
                Name = customer.name,
                Phone = customer.phone,
                CCCD = customer.cccd,
                Gender = customer.gender,
                UserId = customer.id
            };
        }
        // Lấy thông tin khách hàng theo tên
        public DTO_Customer GetCustomerByName(string name)
        {
            var customer = dalTTKH.GetCustomerByName(name);
            if (customer == null) return null;

            return new DTO_Customer
            {
                CustomerId = customer.customer_id,
                Name = customer.name,
                Phone = customer.phone,
                CCCD = customer.cccd,
                Gender = customer.gender,
                UserId = customer.id
            };
        }
        
        // Kiểm tra xem CCCD đã tồn tại trong cơ sở dữ liệu chưa
        public bool IsCCCDExists(string cccd, int? excludeCustomerId = null)
        {
            return dalTTKH.IsCCCDExists(cccd, excludeCustomerId);
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(DTO_Customer dtoCustomer)
        {
            var customer = new Customer
            {
                customer_id = dtoCustomer.CustomerId,
                name = dtoCustomer.Name,
                phone = dtoCustomer.Phone,
                cccd = dtoCustomer.CCCD,
                gender = dtoCustomer.Gender,
                id = dtoCustomer.UserId
            };
            dalTTKH.UpdateCustomer(customer);
        }

        // Thêm khách hàng mới
        public void AddCustomer(DTO_Customer dtoCustomer)
        {
            if (dtoCustomer.UserId <= 0)
                throw new ArgumentException("Vui lòng tạo tài khoản trước khi thêm khách hàng");

            var customer = new Customer
            {
                name = dtoCustomer.Name,
                phone = dtoCustomer.Phone,
                cccd = dtoCustomer.CCCD,
                gender = dtoCustomer.Gender,
                id = dtoCustomer.UserId
            };
            dalTTKH.AddCustomer(customer);
        }
    }
}
