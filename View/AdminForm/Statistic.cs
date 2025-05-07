using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.BLL;

namespace HotelSystem.View.AdminForm
{
    public partial class Statistic: Form
    {
        public Statistic()
        {
            InitializeComponent();
        }

        private void Statistic_Load(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the AdminBLL class
                AdminBLL adminBLL = new AdminBLL();

                // Get the list of occupied room numbers
                List<string> occupiedRooms = adminBLL.GetOccupiedRooms();
                lblResultOccupiedRooms.Text = $"{occupiedRooms.Count}";

                // Get the list of available room numbers
                List<string> availableRooms = adminBLL.GetAvailableRooms();
                lblResultAvailableRooms.Text = $"{availableRooms.Count}";

                // Get the list of all customers
                List<string> allCustomers = adminBLL.GetAllCustomers();
                lblResultTotalCustomers.Text = $"{allCustomers.Count}";

                // Get the total revenue
                decimal totalRevenue = adminBLL.GetAllRevenue();
                lblResultRevenue.Text = $"{totalRevenue:C}"; // Format as currency
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
