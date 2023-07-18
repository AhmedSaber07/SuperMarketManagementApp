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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        int productId = 0;
        BL.Bills billModel = new BL.Bills();
        BL.Products productModel = new BL.Products();

        private void Form3_Load(object sender, EventArgs e)
        {

            ShowProductsTb();
            getNextBillId();
            ShowDB();
            Program.blid = billidtxt.Text;
        }
        void ShowProductsTb(DataTable dt = null)
        {
            if (dt is null)
            {
                DataTable dt1 = new DataTable();
                dt1 = productModel.getAllProducts();
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
            }
        }
        void getNextBillId()
        {
            int nextBillId = 0;
            nextBillId =  billModel.getNextBillIdToUser(Convert.ToInt32(Program.csid),out nextBillId);
            billidtxt.Text = Convert.ToString(nextBillId);
        }
        double CheckAvilableQuntity(int productId)
        {
            double avilableQuntity = 0.0;
            avilableQuntity =  billModel.getAvilableQuntity(productId, out avilableQuntity);
            
            return avilableQuntity;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                    if (Convert.ToDouble(billidtxt.Text) <= 0 || Convert.ToDouble(quntitytxt.Text) <= 0)
                        MessageBox.Show("Please Enter Valid Data");

                    else
                    {
                        if (CheckAvilableQuntity(productId) < Convert.ToDouble(quntitytxt.Text))
                        {
                            MessageBox.Show("This Quntiity Not Avilable");
                        }
                        else
                        {
                            billModel.AddPrductInBill(productId, Convert.ToInt32(billidtxt.Text), productnametxt.Text, Convert.ToDouble(pricetxt.Text), Convert.ToDouble(quntitytxt.Text), Convert.ToInt32(Program.csid));
                            MessageBox.Show("Product Added");
                            clear();
                            ShowProductsTb();
                            ShowDB();
                        }
                    }

            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }
        void clear()
        {
             pricetxt.Text = quntitytxt.Text = totaltxt.Text = productnametxt.Text = searchtxt.Text =  "";
        }
        void ShowDB()
        {
            DataTable dt = new DataTable();
            dt = billModel.getBillDetailsByUserId(Convert.ToInt32(Program.csid), Convert.ToInt32(billidtxt.Text));
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
        }
        bool ProductExistOrNot(int productId)
        {
            bool flag = billModel.checkProductExistOrNotInBill(Convert.ToInt32(Program.csid), Convert.ToInt32(billidtxt.Text), productId);
            return flag;
        }
        double getQuntityAfterAdded(int billId,int userId,int productId)
        {
            double quntity = 0.0;
          quntity =   billModel.getQuntityOfProductInBillAfterAdded(userId, billId, productId,out quntity);
            return quntity;
        }
        private void editbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!billModel.checkProductExistOrNotInBill(Convert.ToInt32(Program.csid), Convert.ToInt32(billidtxt.Text), productId))
                {
                    MessageBox.Show("This Product Not Exist!");
                }
                else
                {
                    double quntityInBillDetails = getQuntityAfterAdded(Convert.ToInt32(billidtxt.Text), Convert.ToInt32(Program.csid), productId);
                    double quntityNeeded = Convert.ToDouble(quntitytxt.Text) - quntityInBillDetails;
                 if ((Convert.ToDouble(quntitytxt.Text) > quntityInBillDetails) && (CheckAvilableQuntity(productId) < quntityNeeded))
                    {
                        MessageBox.Show("This Quntity Not Avilable");
                    }
                    else
                    {

                        billModel.UpdatePrductInBill(productId, Convert.ToInt32(billidtxt.Text), Convert.ToDouble(quntitytxt.Text), Convert.ToDouble(totaltxt.Text), Convert.ToInt32(Program.csid));
                        MessageBox.Show("Product Updated");
                        clear();
                        ShowProductsTb();
                        ShowDB();
                    }
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!billModel.checkProductExistOrNotInBill(Convert.ToInt32(Program.csid), Convert.ToInt32(billidtxt.Text), productId))
                {
                    MessageBox.Show("This Product Not Exist!");
                    clear();
                }
                else
                {
                    billModel.DeleteProductInBill(Convert.ToInt32(billidtxt.Text), Convert.ToInt32(Program.csid),productId, Convert.ToDouble(quntitytxt.Text));
                    MessageBox.Show("Product Deleted");
                    clear();
                    ShowProductsTb();
                    ShowDB();
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }



        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                productId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                pricetxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                productnametxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                totaltxt.Text = "";
                quntitytxt.Text = "";
            }
        }

        private void dataGridView2_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                productId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[6].Value.ToString());
                pricetxt.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                productnametxt.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                quntitytxt.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                totaltxt.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Close();
            f.Show();
        }


        private void printbtn_Click(object sender, EventArgs e)
        {
            try
            {
                billModel.AddInBill(Convert.ToInt32(Program.blid), Convert.ToInt32(Program.csid));
                PL.PrintBillForm printBillForm = new PL.PrintBillForm();
                this.Hide();
                printBillForm.Show();
                Form3_Load(sender, e);
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = productModel.getProductByNameOrCategory(comboBox1.Text);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {
            if (searchtxt.Text == "")
                ShowProductsTb();
            else
            {
                try
                {
                    DataTable dt = productModel.getProductByNameOrCategory(searchtxt.Text);
                    ShowProductsTb(dt);
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }
    }
}
