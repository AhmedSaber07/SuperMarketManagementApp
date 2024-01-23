using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketManagementApp.BL
{
    class Log_in
    {
        public DataTable LOGIN(string userName, string password,string userType)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@userName", SqlDbType.VarChar, 50);
            para[0].Value = userName;

            para[1] = new SqlParameter("@password", SqlDbType.VarChar, 50);
            para[1].Value = password;
            
            para[2] = new SqlParameter("@userType", SqlDbType.VarChar, 50);
            para[2].Value = userType;
            
            DA.open();
            
            DataTable dt = new DataTable();
            dt = DA.stored_Date("LOG_IN", para);
            DA.close();
            return dt;
        }
        
    }
}




