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
    public partial class Form1 : Form
    {
        BL.Log_in log = new BL.Log_in();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signupbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            DataTable dt = log.LOGIN(usernametxt.Text,passwordtxt.Text,comboBox1.Text);
           if(dt.Rows.Count>0)
            {
                var userType = dt.Rows[0][7].ToString();
                string FullName = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                if (userType=="salesman")
                {
                    Program.csid = dt.Rows[0][0].ToString();
                    MessageBox.Show("Welcome " + FullName);
                    this.Hide();
                    Form3 f = new Form3();
                    f.Show();
                }
                else
                {
                    MessageBox.Show($"welcome Mr/ {FullName}");
                    this.Hide();
                    Form4 f = new Form4();
                    f.Show();
                }
            }
            else
            {
                MessageBox.Show("Log in Falid!");
            }
        }
    }
}
