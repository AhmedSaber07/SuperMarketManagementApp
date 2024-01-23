using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketManagementApp.PL
{
    public partial class PrintBillForm : Form
    {
        public PrintBillForm()
        {
            InitializeComponent();
        }
        BL.Bills Bills = new BL.Bills(); 
        private void PrintBillForm_Load(object sender, EventArgs e)
        {
            ShowBillData();
        }
        void ShowBillData()
        {
            DataTable dt = Bills.getBillDetailsByUserId(Convert.ToInt32(Program.csid), Convert.ToInt32(Program.blid));
            DataTable dt2 = dt2 = Bills.getBillByUserId(Convert.ToInt32(Program.blid), Convert.ToInt32(Program.csid));
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dateTextBox.Text = dt2.Rows[0][1].ToString();
            TotalTextBox.Text = dt2.Rows[0][2].ToString();
            BillNumberTextBox.Text = Program.blid;
            UserNameTextBox.Text = Bills.getUserNameById(Convert.ToInt32(Program.csid));
        }
        Bitmap bmp;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(bmp, 0, 0);
        }
        private void printButton_Click(object sender, EventArgs e)
        {
            printButton.Hide();
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y,0,0,this.Size);
            printPreviewDialog1.ShowDialog();
            this.Close();
            Form3 f = new Form3();
            f.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }         

}
