using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Market.BL
{
    class Products
    {
        public void AddPrduct(string name,double price,double quntity,string category)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@name", SqlDbType.VarChar, 50);
            para[0].Value = name;

            para[1] = new SqlParameter("@price", SqlDbType.Float);
            para[1].Value = price;

            para[2] = new SqlParameter("@quntity", SqlDbType.Float);
            para[2].Value = quntity;

            para[3] = new SqlParameter("@category", SqlDbType.VarChar, 50);
            para[3].Value = category;

            DA.ExecuteCommand("AddProduct", para);
            DA.close();
        }
        public void UpdatePrduct(int id , string name, double price, double quntity, string category)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[5];

            para[0] = new SqlParameter("@id", SqlDbType.Int);
            para[0].Value = id;

            para[1] = new SqlParameter("@name", SqlDbType.VarChar, 50);
            para[1].Value = name;

            para[2] = new SqlParameter("@price", SqlDbType.Float);
            para[2].Value = price;

            para[3] = new SqlParameter("@quntity", SqlDbType.Float);
            para[3].Value = quntity;

            para[4] = new SqlParameter("@category", SqlDbType.VarChar, 50);
            para[4].Value = category;

            DA.ExecuteCommand("updateProduct", para);
            DA.close();
        }
        public void DeletePrduct(int id)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", SqlDbType.Int);
            para[0].Value = id;

            DA.ExecuteCommand("deleteProduct",para);
            DA.close();
        }
        public DataTable getProductByNameOrCategory(string name)
        {
            DataTable dt = new DataTable();
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@name", SqlDbType.VarChar,50);
            para[0].Value = name;
            dt = DA.stored_Date("getProductByNameOrCategory", para);
            DA.close();
            return dt;
        }
        public DataTable getAllProducts()
        {
            DataTable dt = new DataTable();
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            dt = DA.stored_Date("getAllProducts",null);
            DA.close();
            return dt;
        }
        public bool checkProductExistOrNot(string name)
        {
            DataTable dt = new DataTable();
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@name", SqlDbType.VarChar,50);
            para[0].Value = name.ToLower();
            DA.open();
            dt = DA.stored_Date("ProductExistOrNot", para);
            DA.close();
            return dt.Rows.Count>0;
        }
    }
}
