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
using HotelSystem.DTO;

namespace HotelSystem.View.StaffForm
{
    public partial class CustomerDetail: Form
    {
        private BLL_TTKH bllTTKH;
        private int _customerId;

        public CustomerDetail(int customerId)
        {
            InitializeComponent();
            bllTTKH = new BLL_TTKH();
            _customerId = customerId;
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            
            // Hiển thị ID khách hàng tiếp theo
            txtCustomerId.Text = _customerId.ToString();
            txtCustomerId.Enabled = false; // Không cho phép sửa ID
               
            var customer = bllTTKH.GetCustomerByCustomerId(_customerId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtPhone.Text = customer.Phone;
                txtCCCD.Text = customer.CCCD;
                if (customer.Gender == true)
                {
                    rbMale.Checked = true;
                }
                else rbFemale.Checked = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (txtPhone.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải là 10 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (txtCCCD.Text.Length != 12)
            {
                MessageBox.Show("CCCD phải là 12 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Xác định giới tính
            bool? gender = null;
            if (rbMale.Checked)
            {
                gender = true;
            }
            else if (rbFemale.Checked)
            {
                gender = false;
            }
            
            // Kiểm tra xem đây là cập nhật hay thêm mới
            var existingCustomer = bllTTKH.GetCustomerByCustomerId(_customerId);
            
            // Kiểm tra CCCD có bị trùng không (trừ khách hàng hiện tại nếu đang cập nhật)
            if (bllTTKH.IsCCCDExists(txtCCCD.Text.Trim(), existingCustomer?.CustomerId))
            {
                MessageBox.Show("CCCD này đã được sử dụng bởi khách hàng khác. Vui lòng kiểm tra lại.", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                if (existingCustomer != null)
                {
                    // Cập nhật khách hàng hiện có
                    existingCustomer.Name = txtName.Text.Trim();
                    existingCustomer.Phone = txtPhone.Text.Trim();
                    existingCustomer.CCCD = txtCCCD.Text.Trim();
                    existingCustomer.Gender = gender;

                    bllTTKH.UpdateCustomer(existingCustomer);
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Thêm khách hàng mới
                    var newCustomer = new DTO_Customer
                    {
                        Name = txtName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        CCCD = txtCCCD.Text.Trim(),
                        Gender = gender
                    };

                    bllTTKH.AddCustomer(newCustomer);
                    MessageBox.Show("Thêm thông tin khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Đóng form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
