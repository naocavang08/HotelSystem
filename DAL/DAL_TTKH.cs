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
        // Lấy thông tin khách hàng theo tên
        public Customer GetCustomerByName(string name)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.FirstOrDefault(c => c.name.ToLower() == name.ToLower());
            }
        }
        // Lấy thông tin khách hàng theo tên và cccd
        public Customer GetCustomerByNameAndCCCD(string name, string cccd)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.FirstOrDefault(c => c.name.ToLower() == name.ToLower() && c.cccd == cccd);
            }
        }
        // Lấy thông tin khách hàng theo ID
        public Customer GetCustomerByCustomerId(int customerId)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers.FirstOrDefault(c => c.customer_id == customerId);
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
        
        // Lấy ID tiếp theo cho khách hàng mới
        public int GetNextCustomerId()
        {
            using (var db = new DBHotelSystem())
            {
                if (db.Customers.Any())
                {
                    return db.Customers.Max(c => c.customer_id) + 1;
                }
                else
                {
                    return 1; // Nếu không có khách hàng nào, bắt đầu từ 1
                }
            }
        }
    }
}
