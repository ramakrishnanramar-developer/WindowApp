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

namespace DecentDesigners
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPasswor.Text;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter user name!");
            }
            else if (!Helpers.IsValidEmail(email))
            {
                MessageBox.Show("Please enter valid user name!");
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Password!");
            }
            else
            {
                UsersCRUD usersService = new UsersCRUD();
                UsersSessionDTO userDetails = usersService.Login(email, password);
                if (userDetails != null)
                {
                    if (userDetails.result == 1)
                    {
                        UserSession.Id = userDetails.Id;
                        UserSession.Name = userDetails.UserName;
                        UserSession.IsAdmin = userDetails.IsAdmin;
                        Administration administration = new Administration();
                        administration.Show();
                        this.Hide();
                    }
                    else
                    {
                        txtPasswor.Text = "";
                        MessageBox.Show("Please try again!");
                    }
                }
                else
                {
                    MessageBox.Show("Please contact adminstrator!");
                }

            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
