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

namespace DecentDesigners.Products
{
    public partial class Categories : Form
    {
        public Categories()
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
                LoadItems();
            }
        }
        public void LoadItems()
        {
            txName.Text = "";
            CategoriesCRUD itemCRUD = new CategoriesCRUD();
            List<CategoriesGridDTO> items = itemCRUD.GetAll();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = items;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void Categories_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width / 2;
            panel2.Width = this.Width / 2;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(lblID.Text);
            string name = txName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter Item name!");
            }
            else
            {
                if (Id == 0)
                {
                    CategoriesCRUD itemService = new CategoriesCRUD();
                    var successFlag = itemService.Save(new CategoriesDTO()
                    {
                        CreatedBy = UserSession.Id,
                        Name = name
                    });
                    if (successFlag == 1)
                    {
                        LoadItems();
                        MessageBox.Show("Item created Successfully!");
                    }
                    else if (successFlag == 2)
                    {
                        MessageBox.Show("Item already exists!, Please try with other Item.");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
                else
                {
                    CategoriesCRUD itemService = new CategoriesCRUD();
                    var successFlag = itemService.Update(new UpdateCategoriesDTO()
                    {
                        CreatedBy = UserSession.Id,
                        Name = name,
                        Id = Id
                    });
                    if (successFlag == 1)
                    {
                        MessageBox.Show("Item Updated Successfully!");
                        LoadItems();
                    }
                    else if (successFlag == 2)
                    {
                        MessageBox.Show("Item already exists!, Please try with other Item.");
                    }
                    else if (successFlag == 404)
                    {
                        MessageBox.Show("Item not found!");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                CategoriesCRUD itemCRUD = new CategoriesCRUD();
                CategoriesGridDTO item = itemCRUD.GetByID(Id);
                txName.Text = item.Name;
                lblID.Text = item.Id.ToString();
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

                    CategoriesCRUD itemCRUD = new CategoriesCRUD();
                    int successFlag = itemCRUD.DeleteByID(Id);
                    if (successFlag == 1)
                    {
                        MessageBox.Show("Item deleted Successfully!");
                        LoadItems();
                    }
                    else if (successFlag == 404)
                    {
                        MessageBox.Show("Item not found!");
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
