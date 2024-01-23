using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketManagementApp.DAL
{
    class DataAccessLayer
    {
        SqlConnection con;
        // this constructor inisialize connection object
        public DataAccessLayer()
        {
            con = new SqlConnection("Data Source=DESKTOP-TDQADM2\\AHMEDSABER;Initial Catalog=SuperMarket;Integrated Security=True");
        }
        // method to open connection
        public void open()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
        }
        // method to close connection
        public void close()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        // method to Read data From Database
        public DataTable stored_Date(string procedure, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            cmd.Connection = con;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // method to insert , update , delete data From Database
        public void ExecuteCommand(string procedure, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            cmd.Connection = con;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
                // This Method Do the same as use for loop
            }
            cmd.ExecuteNonQuery();
        }
    }
}
