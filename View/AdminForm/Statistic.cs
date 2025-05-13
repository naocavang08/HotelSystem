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

            cbbLoaiThongKe.Items.Clear();
            cbbLoaiThongKe.Items.Add("Theo tháng");
            cbbLoaiThongKe.Items.Add("Theo quý");
            cbbLoaiThongKe.Items.Add("Theo năm");
            cbbLoaiThongKe.SelectedIndex = 0; // Chọn mặc định là "Theo tháng"

            cbbThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cbbThang.Items.Add(i.ToString());
            }
            cbbThang.SelectedIndex = 0; 
            

            cbbQuy.Items.Clear();
            cbbQuy.Items.Add("Quý 1");
            cbbQuy.Items.Add("Quý 2");
            cbbQuy.Items.Add("Quý 3");
            cbbQuy.Items.Add("Quý 4");
            cbbQuy.SelectedIndex = 0;
            
        }


    }
}
