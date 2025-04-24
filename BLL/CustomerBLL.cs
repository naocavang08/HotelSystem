using HotelSystem.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.Model;

namespace HotelSystem.BLL
{
    class CustomerBLL
    {
        private CustomerDAL customerDAL = new CustomerDAL();

        public List<Customer> GetCustomers()
        {
            return customerDAL.GetAllCustomers();
        }

        public Customer GetCustomerById(int id)
        {
            return customerDAL.GetCustomerById(id);
        }

        public void AddCustomer(Customer customer)
        {
            // Kiểm tra hợp lệ nếu cần
            customerDAL.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerDAL.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            customerDAL.DeleteCustomer(id);
        }
    }
}
