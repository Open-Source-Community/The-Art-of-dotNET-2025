using System;
using System.Data;
using System.Data.SqlClient;

/* 3Tier Layer Application Version:
 * 1.Data Access Layer (DAL.dll) 
 *      => using Data.SqlClient in this layer ( Handles the connections and retieve or edit on database)
 *      => Take the Stored Procerure Name from BLL and return to it (int or object or data table)
 * 
 * 2.Business Logic Layer (BLL.dll)
 *      => In Most Common Examples contains:
 *          A.Entities: (ex. Employee)
 *          B.Entity List: (ex. EmployeeList)
 *          C.Entity Manager: (ex. EmployeeManager => to handle the business logic function like CRUD)
 *      => Return Business Objects to UI Layer
 *      
 *      **Keep the base class in this layer
 *      
 * 3.UI Layer
 *      => Call Some Function from BLL
 */

namespace DAL
{
    public class DBManager
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataTable dt;

        public DBManager()
        {
            try
            {
                conn = new("Data Source=.;Initial Catalog=Northwind;Integrated Security=True;");
                cmd = new();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                da = new(cmd);
                dt = new();
            }
            catch
            {
                
            }
        }
        public int ExecuteNonQuery(string spName)
        {
            int ret = -1;
            try
            {
                if (conn?.State == ConnectionState.Closed)
                {
                    conn.Open();
                    cmd.CommandText = spName;
                    cmd.Parameters.Clear();
                    ret = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {

            }
            return ret;
        }
        public object ExecuteScalar(string spName)
        {
            object ret = new object();
            try
            {
                if (conn?.State == ConnectionState.Closed)
                {
                    conn.Open();
                    cmd.CommandText = spName;
                    cmd.Parameters.Clear();
                    ret = cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            catch
            {

            }
            return ret;
        }
        public DataTable ExecuteDataTable(string spName)
        {
            dt.Clear();
            try
            {
                cmd.CommandText = spName;
                cmd.Parameters.Clear();
                da.Fill(dt);
            }
            catch
            {

            }
            return dt;
        }
    }
}
 