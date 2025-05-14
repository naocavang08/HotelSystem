using HotelSystem.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace HotelSystem.DAL
{
    public class DAL_TTKH
    {
        // Lấy thông tin khách hàng theo UserId
        public Customer GetCustomerByUserId(int userId)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.user_id == userId);
            }
        }
        // Lấy thông tin khách hàng theo tên
        public Customer GetCustomerByName(string name)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.name == name);
            }
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            using (var db = new DBHotelSystem())
            {
                // Debug
                Console.WriteLine($"DAL - Updating customer {customer.customer_id}");
                Console.WriteLine($"DAL - Gender value to save: {customer.gender}");

                var existingCustomer = db.Customers.FirstOrDefault(c => c.customer_id == customer.customer_id);
                if (existingCustomer != null)
                {
                    Console.WriteLine($"DAL - Current gender in DB: {existingCustomer.gender}");

                    existingCustomer.name = customer.name;
                    existingCustomer.phone = customer.phone;
                    existingCustomer.cccd = customer.cccd;
                    existingCustomer.gender = customer.gender;
                    existingCustomer.user_id = customer.user_id;

                    db.SaveChanges();

                    Console.WriteLine($"DAL - Gender after save: {existingCustomer.gender}");
                }
            }
        }
        // Tìm kiếm khách hàng theo từ khóa
        public List<Customer> SearchCustomers(string keyword)
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers
                    .Include(c => c.User)
                    .Where(c =>
                        c.name.Contains(keyword) ||
                        c.phone.Contains(keyword) ||
                        c.cccd.Contains(keyword))
                    .AsNoTracking()
                    .ToList();
            }
        }
        
        // Lấy danh sách tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            using (var db = new DBHotelSystem())
            {
                return db.Customers
                    .Include(c => c.User)
                    .AsNoTracking()
                    .ToList();
            }
        }
        // Xóa khách hàng theo ID
        public void DeleteCustomer(int customerId)
        {
            using (var db = new DBHotelSystem())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var customer = db.Customers.FirstOrDefault(c => c.customer_id == customerId);
                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        // Thêm khách hàng mới
        public bool AddCustomer(Customer customer)
        {
            using (var db = new DBHotelSystem())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Kiểm tra xem CCCD đã tồn tại chưa
                        if (db.Customers.Any(c => c.cccd == customer.cccd))
                        {
                            throw new Exception("CCCD đã tồn tại trong hệ thống");
                        }

                        // Kiểm tra xem UserId đã tồn tại chưa
                        if (db.Customers.Any(c => c.user_id == customer.user_id))
                        {
                            throw new Exception("Tài khoản này đã được liên kết với khách hàng khác");
                        }

                        db.Customers.Add(customer);
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
