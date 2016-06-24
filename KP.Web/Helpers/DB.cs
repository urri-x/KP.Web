using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;

namespace KP.WebApi.Controllers
{
    static internal class Db
    {
        private static string connectionString;
        public static KPDataContext GetDbContext()
        {
            return new KPDataContext(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            if (!String.IsNullOrEmpty(connectionString))
                return connectionString;

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            if (!ConnectionValid(sqlConnectionStringBuilder))
            {
                sqlConnectionStringBuilder =
                    new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString2"].ToString());

                if (!ConnectionValid(sqlConnectionStringBuilder))
                    throw new ConfigurationErrorsException("Сервер недоступен");
            }
            connectionString = sqlConnectionStringBuilder.ConnectionString;
            return connectionString;
        }

        private static bool ConnectionValid(SqlConnectionStringBuilder connectionStringBuilder)
        {
            var connectTimeout = connectionStringBuilder.ConnectTimeout;
            connectionStringBuilder.ConnectTimeout = 1;
            using (var db = new KPDataContext(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    db.Connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    
                    return false;
                }
                finally
                {
                    connectionStringBuilder.ConnectTimeout = connectTimeout;
                }
            };
            
        }
    }
}