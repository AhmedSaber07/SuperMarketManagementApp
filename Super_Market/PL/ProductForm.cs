using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Super_Market
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        BL.Products productModel = new BL.Products();
        int productId = 0;
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (productcategorytxt.Text == "" || productquntitytxt.Text == "" || productnametxt.Text == "" || productpricetxt.Text == "" )
                MessageBox.Show("Please Complete Data");
            else
            {
                try
                {
                        if (Convert.ToDouble(productquntitytxt.Text) <= 0 || Convert.ToDouble(productpricetxt.Text) <= 0)
                            MessageBox.Show("Please Enter Valid Data");
                        else
                        {
                            if(productModel.checkProductExistOrNot(productnametxt.Text))
                            {
                                MessageBox.Show("This Product Is Already Exist!");
                            }
                            else
                            {
                            productModel.AddPrduct(productnametxt.Text, Convert.ToDouble(productpricetxt.Text), Convert.ToDouble(productquntitytxt.Text), productcategorytxt.Text);
                                MessageBox.Show("Product Added");
                                clear();
                                ShowDB();
                            }
                        }
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }
        void ShowDB(DataTable dt = null)
        {
            if (dt is null)
            {
                DataTable dt1 = productModel.getAllProducts();
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            clear();
            ShowDB();
        }
        void clear()
        {
            productnametxt.Text = productpricetxt.Text = productquntitytxt.Text = productcategorytxt.Text = searchtxt.Text =  "";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                productId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                productnametxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                productcategorytxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                productquntitytxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                productpricetxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
              
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (productcategorytxt.Text == "" || productquntitytxt.Text == "" || productnametxt.Text == "" || productpricetxt.Text == "")
                MessageBox.Show("Please Complete Data");
            else
            {
                try
                {
                    if (!productModel.checkProductExistOrNot(productnametxt.Text))
                    {
                        MessageBox.Show("This Product Not Exist!");
                    }
                    else
                    {
                     productModel.UpdatePrduct(productId, productnametxt.Text, Convert.ToDouble(productpricetxt.Text), Convert.ToDouble(productquntitytxt.Text), productcategorytxt.Text);
                            MessageBox.Show("Product Updated");
                            clear();
                            ShowDB();
                    }
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (productcategorytxt.Text == "" || productquntitytxt.Text == "" || productnametxt.Text == "" || productpricetxt.Text == "")
                MessageBox.Show("Please Complete Data");
            else
            {
                if (!productModel.checkProductExistOrNot(productnametxt.Text))
                {
                    MessageBox.Show("This Product Not Exist!");
                }
                else
                {
                    try
                    {

                        productModel.DeletePrduct(productId);
                            MessageBox.Show("Product Deleted");
                            clear();
                            ShowDB();
                    }

                    catch (Exception z)
                    {
                        MessageBox.Show(z.Message);
                    }
                }
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Close();
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , product name can't contain numbers");
            }
        }

        private void productcategorytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , product category can't contain numbers");
            }
        }

        private void productquntitytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Error , product quntity can't contain letters");
            }
        }

        private void productpricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Error , product price can't contain letters");
            }
        }

        private void searchtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , can't using number to search");
            }
        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
                ShowDB();
            else
            {
                try
                {
                    DataTable dt = productModel.getProductByNameOrCategory(searchtxt.Text);
                    ShowDB(dt);
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }
    }
}
