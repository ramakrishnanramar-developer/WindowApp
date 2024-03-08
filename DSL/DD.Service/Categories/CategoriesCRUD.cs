using DD.DAL;
using DD.DBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Service
{

    public static class CategoriesCRUDMode
    {
        public static int Insert = 1;
        public static int Update = 2;
        public static int Delete = 3;
        public static int UndoDelete = 4;
        public static int GetAll = 5;
        public static int GetById = 6;
        public static int GetAllForDropDown = 7;
    }
    public class CategoriesCRUD
    {
        public static string spName = "sp_Categories_crud";
        public CategoriesCRUD()
        {

        }
        public int Save(CategoriesDTO item)
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
                    new SqlParameter("@mode",CategoriesCRUDMode.Insert),
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
        public int Update(UpdateCategoriesDTO item)
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
                    new SqlParameter("@mode",CategoriesCRUDMode.Update),
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

        public List<CategoriesGridDTO> GetAll()
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
                    new SqlParameter("@mode",CategoriesCRUDMode.GetAll),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<CategoriesGridDTO> rows = new List<CategoriesGridDTO>();
                rows = Helpers.ConvertDataTable<CategoriesGridDTO>(ds.Tables[0]);
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public List<CategoriesGetDTO> GetAllForDropdown()
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
                    new SqlParameter("@mode",CategoriesCRUDMode.GetAllForDropDown),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<CategoriesGetDTO> rows = new List<CategoriesGetDTO>();
                rows = Helpers.ConvertDataTable<CategoriesGetDTO>(ds.Tables[0]);
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public CategoriesGridDTO GetByID(int id)
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
                    new SqlParameter("@mode",CategoriesCRUDMode.GetById),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<CategoriesGridDTO> rows = new List<CategoriesGridDTO>();
                rows = Helpers.ConvertDataTable<CategoriesGridDTO>(ds.Tables[0]);
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
                    new SqlParameter("@mode",CategoriesCRUDMode.Delete),
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
