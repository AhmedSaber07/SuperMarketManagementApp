using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketManagementApp.BL
{
    class Bills
    {
        public void AddPrductInBill(int productId,int billId,string productName, double price, double quntity, int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[6];

            para[0] = new SqlParameter("@productId", SqlDbType.Int);
            para[0].Value = productId;

            para[1] = new SqlParameter("@BillId", SqlDbType.Int);
            para[1].Value = billId;

            para[2] = new SqlParameter("@productName", SqlDbType.VarChar,50);
            para[2].Value = productName;

            para[3] = new SqlParameter("@price", SqlDbType.Float);
            para[3].Value = price;

            para[4] = new SqlParameter("@quntity", SqlDbType.Float);
            para[4].Value = quntity;


            para[5] = new SqlParameter("@userId", SqlDbType.Int);
            para[5].Value = userId;

            DA.ExecuteCommand("AddProductInBill", para);
            DA.close();
        }
        public void UpdatePrductInBill(int productId, int billId, double quntity, double total, int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[5];

            para[0] = new SqlParameter("@productId", SqlDbType.Int);
            para[0].Value = productId;

            para[1] = new SqlParameter("@BillId", SqlDbType.Int);
            para[1].Value = billId;

            para[2] = new SqlParameter("@newQuntity", SqlDbType.Float);
            para[2].Value = quntity;

            para[3] = new SqlParameter("@total", SqlDbType.Float);
            para[3].Value = total;

            para[4] = new SqlParameter("@userId", SqlDbType.Int);
            para[4].Value = userId;

            DA.ExecuteCommand("UpdateProductInBill", para);
            DA.close();
        }
        public void DeleteProductInBill(int billId,int userId,int productId,double quntity)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@BillId", SqlDbType.Int);
            para[0].Value = billId;

            para[1] = new SqlParameter("@userId", SqlDbType.Int);
            para[1].Value = userId;

            para[2] = new SqlParameter("@productId", SqlDbType.Int);
            para[2].Value = productId;

            para[3] = new SqlParameter("@quntity", SqlDbType.Float);
            para[3].Value = quntity;

            DA.ExecuteCommand("DeleteProductInBill", para);
            DA.close();
        }
        public void AddInBill(int billId,int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@BillId", SqlDbType.Int);
            para[0].Value = billId;

            para[1] = new SqlParameter("@userId", SqlDbType.Int);
            para[1].Value = userId;

            DA.ExecuteCommand("calcFinalTotalofBill", para);
            DA.close();
        }
        public int getNextBillIdToUser(int userId,out int nextBillNumber)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dt = new DataTable();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@userId", SqlDbType.Int);
            para[0].Value = userId;
            para[1] = new SqlParameter("@NextBillNumber", SqlDbType.Int);
            para[1].Direction = ParameterDirection.Output;
            DA.open();
           dt =  DA.stored_Date("GetNextBillIdToUser", para);
            DA.close();
            nextBillNumber = Convert.ToInt32(para[1].Value);
            return Convert.ToInt32(nextBillNumber);
        }
        public double getAvilableQuntity(int productId, out double avilableQuntity)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer(); 
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@productId", SqlDbType.Int);
            para[0].Value = productId;
            para[1] = new SqlParameter("@avilableQuntity", SqlDbType.Float);
            para[1].Direction = ParameterDirection.Output;
            DA.open();
            DA.ExecuteCommand("getAvilableQuntityByProductId", para);
            avilableQuntity = Convert.ToDouble(para[1].Value);
            DA.close();
            return avilableQuntity;
        }
        public bool checkProductExistOrNotInBill(int userId, int billId, int productId)
        {
            DataTable dt = new DataTable();
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@userId", SqlDbType.Int);
            para[0].Value = userId;
            para[1] = new SqlParameter("@billId", SqlDbType.Int);
            para[1].Value = billId;
            para[2] = new SqlParameter("@productId", SqlDbType.Int);
            para[2].Value = productId;
            DA.open();
            dt = DA.stored_Date("productExistInBillOrNot", para);
            DA.close();
            return dt.Rows.Count > 0;
        }
        public double getQuntityOfProductInBillAfterAdded(int userId, int billId, int productId , out double quntity)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@userId", SqlDbType.Int);
            para[0].Value = userId;
            para[1] = new SqlParameter("@billId", SqlDbType.Int);
            para[1].Value = billId;
            para[2] = new SqlParameter("@productId", SqlDbType.Int);
            para[2].Value = productId;
            para[3] = new SqlParameter("@quntity", SqlDbType.Float);
            para[3].Direction = ParameterDirection.Output;
            DA.open();
            DA.ExecuteCommand("getQuntityOfProductInBillAfterAdded", para);
            quntity = Convert.ToDouble(para[3].Value);
            DA.close();
            return quntity;
        }
        public DataTable getBillDetailsByUserId(int userId,int billId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dt = new DataTable();
            DA.open();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@billId", SqlDbType.Int);
            para[0].Value = billId;

            para[1] = new SqlParameter("@userId", SqlDbType.Int);
            para[1].Value = userId;

            dt = DA.stored_Date("getBillDetailsByUserId", para);
            DA.close();
            return dt;
        }
        public DataTable getBillByUserId(int billId, int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dt = new DataTable();
            DA.open();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@billId", SqlDbType.Int);
            para[0].Value = billId;

            para[1] = new SqlParameter("@userId", SqlDbType.Int);
            para[1].Value = userId;

            dt = DA.stored_Date("getBillByUserId", para);
            DA.close();
            return dt;
        }
        public string getUserNameById(int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dt = new DataTable();
            DA.open();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@userId", SqlDbType.Int);
            para[0].Value = userId;
            dt = DA.stored_Date("getUserNameByUserId", para);
            var fullName = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
            DA.close();
            return fullName;
        }


    }
}
