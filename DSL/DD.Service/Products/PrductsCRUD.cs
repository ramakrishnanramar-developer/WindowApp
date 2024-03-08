using DD.DAL;
using DD.DBL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Service
{
    public static class ProductsCRUDMode
    {
        public static int Insert = 1;
        public static int Update = 2;
        public static int Delete = 3;
        public static int UndoDelete = 4;
        public static int GetAll = 5;
        public static int GetById = 6;
        public static int GetAllForDropDown = 7;
    }
    public class ProductsCRUD
    {
        public static string spName = "sp_products_crud";
        public int Save(ProductsDTO item)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {   new SqlParameter("@name",item.Name),
                    new SqlParameter("@categoriesId",item.CategoriesId),
                    new SqlParameter("@description",item.Description),
                    new SqlParameter("@mode",ProductsCRUDMode.Insert),
                    new SqlParameter("@createdBy",UserSession.Id)
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                return (int)(ds.Tables[0].Rows[0]["Result"]);
            }
            catch
            {
                return -2;
            }
        }
        public int Update(UpdateProductsDTO item)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {    new SqlParameter("@name",item.Name),
                    new SqlParameter("@categoriesId",item.CategoriesId),
                    new SqlParameter("@description",item.Description),
                    new SqlParameter("@mode",ProductsCRUDMode.Update),
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@id",item.Id)
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                return (int)(ds.Tables[0].Rows[0]["Result"]);
            }
            catch
            {
                return -2;
            }
        }

        public List<GetProductsDTO> GetAll()
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@mode",ProductsCRUDMode.GetAll),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<GetProductsDTO> rows = new List<GetProductsDTO>();
                rows = Helpers.ConvertDataTable<GetProductsDTO>(ds.Tables[0]);
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public List<ProductsListDTO> GetAllForDropdown()
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@mode",ProductsCRUDMode.GetAllForDropDown),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<ProductsListDTO> rows = new List<ProductsListDTO>();
                rows = Helpers.ConvertDataTable<ProductsListDTO>(ds.Tables[0]);
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public GetProductDTO GetByID(int id)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@Id",id),
                    new SqlParameter("@mode",ProductsCRUDMode.GetById),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<GetProductDTO> rows = new List<GetProductDTO>();
                rows = Helpers.ConvertDataTable<GetProductDTO>(ds.Tables[0]);
                return rows.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public int DeleteByID(int id)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@Id",id),
                    new SqlParameter("@mode",ProductsCRUDMode.Delete),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                return (int)(ds.Tables[0].Rows[0]["Result"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
    }
}
