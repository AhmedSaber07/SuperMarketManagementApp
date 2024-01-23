using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketManagementApp.BL
{
    class SignUp
    {
        public void Add_User(string fname, string lname ,string email , string phone , string uname, string pwd)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[7];

            para[0] = new SqlParameter("@fname", SqlDbType.VarChar, 50);
            para[0].Value = fname;

            para[1] = new SqlParameter("@lname", SqlDbType.VarChar, 50);
            para[1].Value = lname;

            para[2] = new SqlParameter("@email", SqlDbType.VarChar, 50);
            para[2].Value = email;

            para[3] = new SqlParameter("@phone", SqlDbType.VarChar, 50);
            para[3].Value = phone;

            para[4] = new SqlParameter("@uname", SqlDbType.VarChar, 50);
            para[4].Value = uname;

            para[5] = new SqlParameter("@pwd", SqlDbType.VarChar, 50);
            para[5].Value = pwd;

            para[6] = new SqlParameter("@userType", SqlDbType.VarChar, 50);
            para[6].Value = "salesman";

            DA.ExecuteCommand("signUp", para);
            DA.close();
        }
        public  DataTable CheckUserName(string userName)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@uname", SqlDbType.VarChar, 50);
            para[0].Value = userName;
            DA.open();
            DataTable dt = new DataTable();
            dt = DA.stored_Date("UserNameExistOrNot", para);
            DA.close();
            return dt;
        }
        public DataTable CheckEmail(string email)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@email", SqlDbType.VarChar, 50);
            para[0].Value = email;
            DA.open();
            DataTable dt = new DataTable();
            dt = DA.stored_Date("EmailExistOrNot", para);
            DA.close();
            return dt;
        }
        public DataTable CheckPhoneNumber(string phoneNumber)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@phone", SqlDbType.VarChar, 50);
            para[0].Value = phoneNumber;
            DA.open();
            DataTable dt = new DataTable();
            dt = DA.stored_Date("PhoneExistOrNot", para);
            DA.close();
            return dt;
        }
    }
}
