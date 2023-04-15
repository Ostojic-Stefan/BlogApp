using System.Data;
using Microsoft.Data.Sqlite;

namespace api.Context
{
    public class SqlContext : ISqlContext
    {
        private readonly IConfiguration _config;

        public SqlContext(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetSqlConnection()
        {
            return new SqliteConnection(_config.GetConnectionString("SqlConnection"));
        }
    }
}