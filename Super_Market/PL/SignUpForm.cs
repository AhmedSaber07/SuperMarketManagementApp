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
using System.Text.RegularExpressions;

namespace Super_Market
{
    public partial class Form2 : Form
    {
        BL.SignUp signUp = new BL.SignUp();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {

            confirmpasswordtxt_Validated(sender, e);
            DataTable dt1 = signUp.CheckUserName(usernametxt.Text);
            DataTable dt2 = signUp.CheckPhoneNumber(phonenumbertxt.Text);
            DataTable dt3 = signUp.CheckEmail(emailtxt.Text);
            if (!IsValiName(firstnametxt.Text))
                MessageBox.Show("Please Enter Valid Name!");
            else if (!IsValiName(lastnametxt.Text))
                MessageBox.Show("Please Enter Valid Name!");
            else if (!IsEmailValid(emailtxt.Text))
                MessageBox.Show("Please Enter Valid Email!");
            else if (dt3.Rows.Count > 0)
                MessageBox.Show("Email is Already Exist");
            else if (!IsValidPhoneNumber(phonenumbertxt.Text))
                MessageBox.Show("Please Enter Valid PhoneNumber!");
            else if (dt2.Rows.Count > 0)
                MessageBox.Show("PhoneNumber is Already Exist!");
            else if (!IsValidUsername(usernametxt.Text))
                MessageBox.Show("Please Enter Valid UserName!");
            else if (dt1.Rows.Count > 0)
                MessageBox.Show("UserName is Already Exist!");
            else if (!IsPasswordValid(passwordtxt.Text))
                MessageBox.Show("Please Enter Valid Password!");
            else
            {
                signUp.Add_User(firstnametxt.Text, lastnametxt.Text, emailtxt.Text, phonenumbertxt.Text, usernametxt.Text, passwordtxt.Text);
                MessageBox.Show("Register Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Form1 f = new Form1();
                f.Show();
            }
        }
        

        private void phonenumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , A phone number can't contain letters");
            }
       }

        private void firstnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , A first name can't contain numbers");
            }
        }

        private void lastnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , A last name can't contain numbers");
            }
        }

        /*private void usernametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error , A user name can't contain numbers");
            }
        }*/
        private bool IsPasswordValid(string password)
        {
            string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";


            Regex regex = new Regex(pattern);
            Match match = regex.Match(password);

            return match.Success;
        }
        private bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);

            return match.Success;
        }
        static bool IsValiName(string Name)
        {
            string pattern = @"^[a-zA-Z]{3,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Name);
        }
        static bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z_][a-zA-Z0-9_]{2,19}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }
        static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(010|011|012|015)\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void confirmpasswordtxt_Validated(object sender, EventArgs e)
        {
            if(passwordtxt.Text != confirmpasswordtxt.Text)
                MessageBox.Show("Password Not Match", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
