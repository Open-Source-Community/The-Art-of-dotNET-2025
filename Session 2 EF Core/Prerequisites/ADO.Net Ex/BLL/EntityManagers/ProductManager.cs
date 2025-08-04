using BLL.Entities;
using BLL.EntityLists;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BLL.EntityManagers
{
    public class ProductManager
    {
        static DBManager dbManager = new DBManager();
        public static ProductList selectAllProducts()
        {
            ProductList list = new ProductList();
            try
            {
                list = DataTableToProductList(
                dbManager.ExecuteDataTable("selectAllProducts")
                );

            }
            catch 
            {
                
            }
            return list;
        }


        #region Mapping Functions
        internal static ProductList DataTableToProductList(DataTable dt)
        {
            ProductList productList = new ProductList();
            try
            {
                if(dt?.Rows?.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                        productList.Add(DataRowToProduct(dr));
                }
            }
            catch
            {

            }

            return productList;
        }

        internal static Product DataRowToProduct(DataRow dr)
        {
            int tmp;
            short tmpShort;
            Product product = new Product();
            try
            {
                //Safe approach
                if (int.TryParse(dr["CategoryID"]?.ToString() ?? "0", out tmp))
                    product.CategoryID = tmp;

                if (bool.TryParse(dr["Discontinued"]?.ToString() ?? "False", out bool tmpBool))
                    product.Discontinued = tmpBool;

                product.ProductID = dr.Field<int>("ProductID"); //Strongly type -> exception when wrong type !!

                if (int.TryParse(dr["SupplierID"]?.ToString() ?? "0", out tmp))
                    product.SupplierID = tmp;

                if (decimal.TryParse(dr["UnitPrice"]?.ToString() ?? "0", out decimal tmpD))
                    product.UnitPrice = tmpD;

                if (short.TryParse(dr["UnitsInStock"]?.ToString() ?? "0", out tmpShort))
                    product.UnitsInStock = tmpShort;

                if (short.TryParse(dr["SupplierID"]?.ToString() ?? "0", out tmpShort))
                    product.SupplierID = tmpShort;

                if (short.TryParse(dr["ReorderLevel"]?.ToString() ?? "0", out tmpShort))
                    product.ReorderLevel = tmpShort;

                product.ProductName = dr["ProductName"]?.ToString() ?? "NA";

                product.QuantityPerUnit = dr["QuantityPerUnit"]?.ToString() ?? "NA";
            }
            catch
            {

            }

            return product;
        }
        #endregion
    }
}
