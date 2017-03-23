using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace myBills.web.Data.Dapper
{
    public static class DapperUtils
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["OmegaConnectionString"].ConnectionString;
        public static IEnumerable<T> GetItems<T>(string sql)
        {
            //ToDo: get 'sql' from T properties, possibly use data annotations
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return sqlConnection.Query<T>(sql);
            }
        }

        public static int InsertItem(string sql, object item)
        {
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                return sqlConnection.Execute(sql, item);
            }
        }

        public static int DeleteItem(string sql, string key)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return sqlConnection.Execute(sql, new { key });
            }
        }
    }
}