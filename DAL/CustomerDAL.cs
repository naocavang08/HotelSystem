using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.DTO;
using HotelSystem.Model;

namespace HotelSystem.Controller
{
    class CustomerDAL
    {
        private DBHotelSystem db = new DBHotelSystem();

        // Lấy tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        // Tìm khách hàng theo ID
        public Customer GetCustomerById(int id)
        {
            return db.Customers.Find(id);
        }

        // Thêm khách hàng
        public void AddCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        // Cập nhật khách hàng
        public void UpdateCustomer(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Xóa khách hàng
        public void DeleteCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }
    }
}
