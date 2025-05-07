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

        public AdminBLL()
        {
            _adminDal = new AdminDAL();
        }

        public List<AdminDTO> GetAllAdmins()
        {
            return _adminDal.GetAdmins();
        }

        public void CreateAdmin(string username, string password)
        {
            var admin = new AdminDTO
            {
                Username = username,
                Password = password
            };
            _adminDal.AddAdmin(admin);
        }

        public void UpdateAdmin(int adminId, string username, string password)
        {
            var admin = new AdminDTO
            {
                AdminId = adminId,
                Username = username,
                Password = password
            };
            _adminDal.UpdateAdmin(admin);
        }

        public void DeleteAdmin(int adminId)
        {
            _adminDal.DeleteAdmin(adminId);
        }
    }
}
