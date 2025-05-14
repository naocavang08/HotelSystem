using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.DAL;
using HotelSystem.View;
using HotelSystem.View.AdminForm;
using HotelSystem.View.CustomerForm;

namespace HotelSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new AdminForm());
            //Application.Run(new Customer());
            //Application.Run(new Room());
            //Application.Run(new Staff());
            //Application.Run(new Statistic());
        }
    }
}
