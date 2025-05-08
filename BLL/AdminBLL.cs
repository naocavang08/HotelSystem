using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.DAL;
using HotelSystem.DTO;



namespace HotelSystem.BLL
{
    internal class AdminBLL
    {
        private readonly AdminDAL _adminDal;

        // Add the missing method definition
        public List<string> GetOccupiedRooms()
        {
            // Assuming AdminDAL has a method to fetch occupied rooms
            return _adminDal.GetOccupiedRooms();
        }
        public List<string> GetAvailableRooms()
        {
            // Assuming AdminDAL has a method to fetch occupied rooms
            return _adminDal.GetOccupiedRooms();
        }
        public List<string> GetAllCustomers()
        {
            // Assuming AdminDAL has a method to fetch occupied rooms
            return _adminDal.GetOccupiedRooms();
        }
        public decimal GetAllRevenue()
        {
            // Assuming AdminDAL has a method to fetch occupied rooms
            return _adminDal.GetAllRevenue();
        }












        public AdminBLL()
        {
            _adminDal = new AdminDAL();
        }

    }
}
