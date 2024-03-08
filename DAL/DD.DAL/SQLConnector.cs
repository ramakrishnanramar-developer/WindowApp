using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DAL
{
    public class SQLConnector
    {
        public const string connectionString = "Server=tcp:decentdesigners.database.windows.net,1433;Initial Catalog=decentdesigners;Persist Security Info=False;User ID=ramakrishnan;Password=Ram#@#@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DataSet ExecuteWithParam(string spName, List<SqlParameter> sqlParameters)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        if (sqlParameters != null)
                        {
                            foreach (var param in sqlParameters)
                            {
                                cmd.Parameters.Add(param);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                }
                return ds;
            }
        }

        public int SubmitExecuteWithParam(string spName, List<SqlParameter> sqlParameters)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        if (sqlParameters != null)
                        {
                            foreach (var param in sqlParameters)
                            {
                                cmd.Parameters.Add(param);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
