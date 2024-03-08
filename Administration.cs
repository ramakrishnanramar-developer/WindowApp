using DD.DBL;
using DecentDesigners.Products;
using DecentDesigners.User_Forms;
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
    public partial class Administration : Form
    {

        public Administration()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            int width = Screen.GetWorkingArea(this).Width;
            btnClose.Location = new System.Drawing.Point(width - 40, 0);
            PageLoad();
            btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            btnUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
        }

        private void Users_Load(object sender, EventArgs e)
        {
        }


        private void btnUsers_Click(object sender, EventArgs e)
        {
            PageLoad();
            BackColor(btnUsers);
        }
        private void BackColor(System.Windows.Forms.Button button)
        {
            btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            btnUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            btnProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            btnProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            btnCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            btnCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void PageLoad()
        {
            pnlLayout.Controls.Clear();
            UserList userList = new UserList() { TopLevel = false, TopMost = true };
            userList.FormBorderStyle = FormBorderStyle.None;
            pnlLayout.Controls.Add(userList);
            userList.Width = pnlLayout.Width;
            userList.Height = pnlLayout.Height;
            userList.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.IsAdmin = false;
            UserSession.Id = 0;
            UserSession.Name = "";
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            pnlLayout.Controls.Clear();
            Categories item = new Categories() { TopLevel = false, TopMost = true };
            item.FormBorderStyle = FormBorderStyle.None;
            pnlLayout.Controls.Add(item);
            item.Width = pnlLayout.Width;
            item.Height = pnlLayout.Height;
            item.Show();
            BackColor(btnCategories);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            pnlLayout.Controls.Clear();
            ProductsForm item = new ProductsForm() { TopLevel = false, TopMost = true };
            item.FormBorderStyle = FormBorderStyle.None;
            pnlLayout.Controls.Add(item);
            item.Width = pnlLayout.Width;
            item.Height = pnlLayout.Height;
            item.Show();
            BackColor(btnProduct);
        }
    }
}
