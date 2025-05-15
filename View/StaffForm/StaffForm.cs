using HotelSystem.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem.View.StaffForm
{
    public partial class StaffForm: Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }

        private void picCusInfo_Click(object sender, EventArgs e)
        {
            CustomerList op = new CustomerList();
            op.Show();

            this.Hide();
        }

        private void lblCusInfo_Click(object sender, EventArgs e)
        {
            CustomerList op = new CustomerList();
            op.Show();

            this.Hide();
        }

        private void picInvoice_Click(object sender, EventArgs e)
        {
            InvoiceForm op = new InvoiceForm();
            op.Show();

            this.Hide();
        }

        private void lblInvoice_Click(object sender, EventArgs e)
        {
            InvoiceForm op = new InvoiceForm();
            op.Show();

            this.Hide();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào, {UserSession.Username}!";
        }
    }
}
