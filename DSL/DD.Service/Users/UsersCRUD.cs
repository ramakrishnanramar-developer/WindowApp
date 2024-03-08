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

    public static class UsersCRUDMode
    {
        public static int Insert = 1;
        public static int Update = 2;
        public static int Delete = 3;
        public static int UndoDelete = 4;
        public static int ProvideAdminAccess = 5;
        public static int DeleteAdminAccess = 6;
        public static int GetUsers = 7;
        public static int GetUsersById = 8;
    }
    public class UsersCRUD
    {
        public static string spName = "sp_Users_crud";
        public static string loginSPName = "sp_Users_login";
        public UsersCRUD()
        {

        }
        public int Save(UsersCRUDDTO users)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {   new SqlParameter("@username",users.UserName),
                    new SqlParameter("@password",Helpers.Encrypt(users.Password)),
                    new SqlParameter("@isAdmin",users.IsAdmin),
                    new SqlParameter("@mode",UsersCRUDMode.Insert),
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
        public int Update(UsersCRUDUpdateDTO users)
        {
            if (UserSession.Id == 0)
            {
                throw new Exception("Please signout and login again!");
            }
            SQLConnector sql = new SQLConnector();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {   new SqlParameter("@username",users.UserName),
                    new SqlParameter("@password",Helpers.Encrypt(users.Password)),
                    new SqlParameter("@isAdmin",users.IsAdmin),
                    new SqlParameter("@mode",UsersCRUDMode.Update),
                    new SqlParameter("@createdBy",UserSession.Id),
                    new SqlParameter("@id",users.Id),
                    new SqlParameter("@isPasswordReset",users.isPasswordReset)
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                return (int)(ds.Tables[0].Rows[0]["Result"]);
            }
            catch
            {
                return -2;
            }
        }

        public List<UserGridDTO> GetUsers()
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
                    new SqlParameter("@mode",UsersCRUDMode.GetUsers),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<UserGridDTO> rows = new List<UserGridDTO>();
                rows = Helpers.ConvertDataTable<UserGridDTO>(ds.Tables[0]);
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public UserGridDTO GetUsersByID(int id)
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
                    new SqlParameter("@mode",UsersCRUDMode.GetUsersById),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                List<UserGridDTO> rows = new List<UserGridDTO>();
                rows = Helpers.ConvertDataTable<UserGridDTO>(ds.Tables[0]);
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
                    new SqlParameter("@mode",UsersCRUDMode.Delete),
                };
                DataSet ds = sql.ExecuteWithParam(spName, sqlParameters);
                return (int)(ds.Tables[0].Rows[0]["Result"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
        public UsersSessionDTO Login(string userName, string password)
        {
            SQLConnector sql = new SQLConnector();
            try
            {
                password = Helpers.Encrypt(password);
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {   new SqlParameter("@username",userName),
                    new SqlParameter("@password",password)
                };
                DataSet ds = sql.ExecuteWithParam(loginSPName, sqlParameters);

                List<UsersSessionDTO> rows = new List<UsersSessionDTO>();
                rows = Helpers.ConvertDataTable<UsersSessionDTO>(ds.Tables[0]);
                return rows.FirstOrDefault();
            }
            catch
            {
                throw new Exception("Please contact Adminsitrator!");
            }
        }
    }
}
