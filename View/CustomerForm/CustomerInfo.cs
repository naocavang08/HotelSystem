using HotelSystem.BLL;
using HotelSystem.DTO;
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

namespace HotelSystem.View.CustomerForm
{
    public partial class CustomerInfo : Form
    {
        Form callerForm;
        public CustomerInfo(Form caller)
        {
            InitializeComponent();
            this.callerForm = caller;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (callerForm != null)
            {
                callerForm.Show();
            }

            this.Close();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải là 10 ký tự.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtCCCD.Text.Length != 12)
            {
                MessageBox.Show("CCCD phải là 12 ký tự.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            bool? gender = null;
            if (rbMale.Checked)
            {
                gender = true;
            }
            else if (rbFemale.Checked)
            {
                gender = false;
            }

            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            
            if (customer == null)
            {
                var newCustomer = new DTO_Customer
                {
                    Name = txtName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    CCCD = txtCCCD.Text.Trim(),
                    Gender = gender,
                    UserId = UserSession.UserId
                };

                bllTTKH.AddCustomer(newCustomer);
                MessageBox.Show("Thêm thông tin thành công.");
            }
            else
            {
                customer.Name = txtName.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();
                customer.CCCD = txtCCCD.Text.Trim();
                customer.Gender = gender;

                bllTTKH.UpdateCustomer(customer);
                MessageBox.Show("Cập nhật thông tin thành công.");
            }
            CustomerForm customerForm = new CustomerForm();
            customerForm.Show();
            this.Close();
        }

        private void CustomerInfo_Load(object sender, EventArgs e)
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            if (customer != null) 
            {
                txtID.Text = customer.CustomerId.ToString();
                txtName.Text = customer.Name;
                txtPhone.Text = customer.Phone;
                txtCCCD.Text = customer.CCCD;
                if (customer.Gender == true) rbMale.Checked = true;
                if (customer.Gender == false) rbFemale.Checked = true;
            }
        }
    }
}
