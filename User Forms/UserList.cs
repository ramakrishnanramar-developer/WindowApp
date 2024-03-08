using DD.DBL;
using DD.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecentDesigners.User_Forms
{
    public partial class UserList : Form
    {
        public UserList()
        {
            InitializeComponent();            
            if (!UserSession.IsAdmin)
            {
                panel1.Visible = false;
                panel2.Visible = false;
                lblNoAccess.Visible = true;
            }
            else
            {
                LoadUsers();
            }
        }
        public void LoadUsers()
        {
            UsersCRUD usersCRUD = new UsersCRUD();
            List<UserGridDTO> users = usersCRUD.GetUsers();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = users;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            txtConfirmPassword.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            cbIsAdmin.Checked = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(lblID.Text);
            bool isPasswordReset = cbResetPassword.Checked;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter user name!");
            }
            else if (!Helpers.IsValidEmail(email))
            {
                MessageBox.Show("Please enter valid user name!");
            }
            else if (string.IsNullOrEmpty(password) && (Id == 0 || isPasswordReset))
            {
                MessageBox.Show("Please enter Password!");
            }
            else if (string.IsNullOrEmpty(confirmPassword) && (Id == 0 || isPasswordReset))
            {
                MessageBox.Show("Please enter confirm Password!");
            }
            else if (password != confirmPassword && (Id == 0 || isPasswordReset))
            {
                MessageBox.Show("Please enter same Password!");
            }
            else
            {
                if (Id == 0)
                {
                    UsersCRUD usersService = new UsersCRUD();
                    var successFlag = usersService.Save(new UsersCRUDDTO()
                    {
                        CreatedBy = UserSession.Id,
                        IsAdmin = cbIsAdmin.Checked,
                        Password = password,
                        UserName = email
                    });
                    if (successFlag == 1)
                    {
                        LoadUsers();
                        MessageBox.Show("User created Successfully!");
                    }
                    else if (successFlag == 2)
                    {
                        MessageBox.Show("User already exists!, Please try with other email.");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
                else
                {
                    UsersCRUD usersService = new UsersCRUD();
                    var successFlag = usersService.Update(new UsersCRUDUpdateDTO()
                    {
                        CreatedBy = UserSession.Id,
                        IsAdmin = cbIsAdmin.Checked,
                        Password = password,
                        UserName = email,
                        isPasswordReset = isPasswordReset,
                        Id = Id
                    });
                    if (successFlag == 1)
                    {
                        MessageBox.Show("User Updated Successfully!");
                        LoadUsers();
                        cbResetPassword.Visible = false;
                        cbResetPasswordAction();
                    }
                    else if (successFlag == 2)
                    {
                        MessageBox.Show("User already exists!, Please try with other email.");
                    }
                    else if (successFlag == 404)
                    {
                        MessageBox.Show("User not found!");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
            }
        }

        private void UserList_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width / 2;
            panel2.Width = this.Width / 2;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                UsersCRUD usersCRUD = new UsersCRUD();
                UserGridDTO users = usersCRUD.GetUsersByID(Id);
                txtConfirmPassword.Text = "";
                txtEmail.Text = users.UserName;
                txtPassword.Text = "";
                if (users.IsAdmin)
                {
                    cbIsAdmin.Checked = true;
                }
                else
                {
                    cbIsAdmin.Checked = false;
                }
                lblID.Text = users.Id.ToString();
                cbResetPassword.Visible = true;
                cbResetPasswordAction();
            }
        }

        private void cbResetPassword_CheckedChanged(object sender, EventArgs e)
        {
            cbResetPasswordAction();
        }
        private void cbResetPasswordAction()
        {
            if (cbResetPassword.Visible && !cbResetPassword.Checked)
            {
                txtConfirmPassword.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtConfirmPassword.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to Delete?", "Decent Designers", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something

                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                    UsersCRUD usersCRUD = new UsersCRUD();
                    int successFlag = usersCRUD.DeleteByID(Id);
                    if (successFlag == 1)
                    {
                        MessageBox.Show("User deleted Successfully!");
                        LoadUsers();
                        cbResetPassword.Visible = false;
                        cbResetPasswordAction();
                    }
                    else if (successFlag == 404)
                    {
                        MessageBox.Show("User not found!");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
    }
}
