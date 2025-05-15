using HotelSystem.Model;
using System.Collections.Generic;
using System.Linq;

namespace HotelSystem.DAL
{
    public class DAL_TTKH
    {
        // Lấy danh sách tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.ToList();
            }
        }
        // Lấy thông tin khách hàng theo UserId
        public Customer GetCustomerByUserId(int userId)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.FirstOrDefault(c => c.id == userId);
            }
        }
        // Lấy thông tin khách hàng theo tên
        public Customer GetCustomerByName(string name)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.FirstOrDefault(c => c.name == name);
            }
        }
        
        // Kiểm tra xem CCCD đã tồn tại trong cơ sở dữ liệu chưa
        public bool IsCCCDExists(string cccd, int? excludeCustomerId = null)
        {
            using (var db = new DBHotelSystem())
            {
                if (excludeCustomerId.HasValue)
                {
                    return db.Customers.Any(c => c.cccd == cccd && c.customer_id != excludeCustomerId);
                }
                else
                {
                    return db.Customers.Any(c => c.cccd == cccd);
                }
            }
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            using (var db = new DBHotelSystem())
            {
                var existingCustomer = db.Customers.FirstOrDefault(c => c.customer_id == customer.customer_id);
                if (existingCustomer != null)
                {
                    existingCustomer.name = customer.name;
                    existingCustomer.phone = customer.phone;
                    existingCustomer.cccd = customer.cccd;
                    existingCustomer.gender = customer.gender;
                    db.SaveChanges();
                }
            }
        }

        // Thêm khách hàng mới
        public void AddCustomer(Customer customer)
        {
            using (var db = new DBHotelSystem())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }
    }
}
