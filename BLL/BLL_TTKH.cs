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
    public class BLL_TTKH
    {
        private DAL_TTKH dalTTKH = new DAL_TTKH();

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
                UserId = customer.user_id
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
                UserId = customer.user_id
            };
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(DTO_Customer dtoCustomer)
        {
            Console.WriteLine($"BLL - Updating customer {dtoCustomer.CustomerId}");
            Console.WriteLine($"BLL - Gender value: {dtoCustomer.Gender}");

            var customer = new Customer
            {
                customer_id = dtoCustomer.CustomerId,
                name = dtoCustomer.Name,
                phone = dtoCustomer.Phone,
                cccd = dtoCustomer.CCCD,
                gender = dtoCustomer.Gender,
                user_id = dtoCustomer.UserId
            };

            // Debug
            Console.WriteLine($"BLL - Converted to Customer model - Gender: {customer.gender}");

            dalTTKH.UpdateCustomer(customer);
        }

        // Thêm khách hàng mới
        public bool AddCustomer(DTO_Customer dtoCustomer)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(dtoCustomer.Name))
                    throw new ArgumentException("Họ tên không được để trống");
                if (string.IsNullOrWhiteSpace(dtoCustomer.Phone))
                    throw new ArgumentException("Số điện thoại không được để trống");
                if (dtoCustomer.Phone.Length != 10)
                    throw new ArgumentException("Số điện thoại phải có 10 số");
                if (string.IsNullOrWhiteSpace(dtoCustomer.CCCD))
                    throw new ArgumentException("CCCD không được để trống");
                if (dtoCustomer.CCCD.Length != 12)
                    throw new ArgumentException("CCCD phải có 12 số");
                if (dtoCustomer.UserId <= 0)
                    throw new ArgumentException("Vui lòng tạo tài khoản trước khi thêm khách hàng");

                // Debug line
                Console.WriteLine($"Adding customer with UserId: {dtoCustomer.UserId}");

                var customer = new Customer
                {
                    name = dtoCustomer.Name,
                    phone = dtoCustomer.Phone,
                    cccd = dtoCustomer.CCCD,
                    gender = dtoCustomer.Gender,
                    user_id = dtoCustomer.UserId
                };

                bool result = dalTTKH.AddCustomer(customer);
                if (result)
                {
                    dtoCustomer.CustomerId = customer.customer_id;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm khách hàng: {ex.Message}", ex);
            }
        }
        public List<DTO_Customer> GetAllCustomers()
        {
            return dalTTKH.GetAllCustomers().Select(c => new DTO_Customer
            {
                CustomerId = c.customer_id,
                Name = c.name,
                Phone = c.phone,
                CCCD = c.cccd,
                Gender = c.gender,
                UserId = c.user_id
            }).ToList();
        }
        public List<DTO_Customer> SearchCustomers(string keyword)
        {
            return dalTTKH.SearchCustomers(keyword).Select(c => new DTO_Customer
            {
                CustomerId = c.customer_id,
                Name = c.name,
                Phone = c.phone,
                CCCD = c.cccd,
                Gender = c.gender,
                UserId = c.user_id
            }).ToList();
        }

        public void DeleteCustomer(int customerId)
        {
            dalTTKH.DeleteCustomer(customerId);
        }
    }
}
