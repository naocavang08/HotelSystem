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
        private int _userId;

        public CustomerInfo()
        {
            InitializeComponent();
            _userId = UserSession.UserId;
        }

        public CustomerInfo(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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

            try
            {
                var bllTTKH = new BLL_TTKH();
                var customer = bllTTKH.GetCustomerByUserId(_userId);
                
                if (customer == null)
                {
                    var newCustomer = new DTO_Customer
                    {
                        Name = txtName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        CCCD = txtCCCD.Text.Trim(),
                        Gender = gender,
                        UserId = _userId
                    };

                    if (bllTTKH.AddCustomer(newCustomer))
                    {
                        MessageBox.Show("Thêm thông tin thành công.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    customer.Name = txtName.Text.Trim();
                    customer.Phone = txtPhone.Text.Trim();
                    customer.CCCD = txtCCCD.Text.Trim();
                    customer.Gender = gender;

                    bllTTKH.UpdateCustomer(customer);
                    MessageBox.Show("Cập nhật thông tin thành công.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerInfo_Load(object sender, EventArgs e)
        {
            txtID.Text = _userId.ToString();
            
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(_userId);
            if (customer != null) 
            {
                txtName.Text = customer.Name;
                txtPhone.Text = customer.Phone;
                txtCCCD.Text = customer.CCCD;
                if (customer.Gender == true) rbMale.Checked = true;
                if (customer.Gender == false) rbFemale.Checked = true;
            }
        }
    }
}
