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
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
            LoadItem();
        }

        private void Products_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width / 2;
            panel2.Width = this.Width / 2;
        }
        private void LoadItem()
        {
            CategoriesCRUD categories = new CategoriesCRUD();
            List<CategoriesGetDTO> items = categories.GetAllForDropdown();
            List<CategoriesGetDTO> _items = new List<CategoriesGetDTO>();
            _items.Add(new DD.DBL.CategoriesGetDTO() { Id = 0, Name = "Select" });
            _items.AddRange(items);
            comboBox1.DataSource = _items;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";


            txName.Text = "";
            comboBox1.SelectedValue = 0;
            txtDescription.Text = "";
            lblID.Text = "0";
            ProductsCRUD itemCRUD = new ProductsCRUD();
            List<GetProductsDTO> dgvitems = itemCRUD.GetAll();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dgvitems;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txName.Text;
            string description = txtDescription.Text;
            int categoriesId =Convert.ToInt32(comboBox1.SelectedValue);
            int Id = Convert.ToInt32(lblID.Text);
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter Item name!");
            }
            else if (categoriesId == 0)
            {
                MessageBox.Show("Please select Categories!");
            }
            else
            {
                if (Id == 0)
                {
                    ProductsCRUD itemService = new ProductsCRUD();
                    var successFlag = itemService.Save(new ProductsDTO()
                    {
                        Name = name,
                        CategoriesId = categoriesId,
                        Description = description
                    });
                    if (successFlag == 1)
                    {
                        LoadItem();
                        MessageBox.Show("Item created Successfully!");
                    }
                    else if (successFlag == 2)
                    {
                        MessageBox.Show("Item already exists!, Please try with other Item.");
                    }
                    else if (successFlag == 3)
                    {
                        MessageBox.Show("Please select categories!");
                    }
                    else
                    {
                        MessageBox.Show("Please contact Administrator!");
                    }
                }
                else
                {
                    ProductsCRUD itemService = new ProductsCRUD();
                    var successFlag = itemService.Update(new UpdateProductsDTO()
                    {
                        Name = name,
                        CategoriesId = categoriesId,
                        Description = description,
                        Id = Id
                    });
                    if (successFlag == 1)
                    {
                        MessageBox.Show("Item Updated Successfully!");
                        LoadItem();
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

                ProductsCRUD itemCRUD = new ProductsCRUD();
                GetProductDTO item = itemCRUD.GetByID(Id);
                txName.Text = item.Name;
                lblID.Text = item.Id.ToString();
                txtDescription.Text = item.Description;
                comboBox1.SelectedValue = item.CategoriesId;
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

                    ProductsCRUD itemCRUD = new ProductsCRUD();
                    int successFlag = itemCRUD.DeleteByID(Id);
                    if (successFlag == 1)
                    {
                        MessageBox.Show("Item deleted Successfully!");
                        LoadItem();
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
