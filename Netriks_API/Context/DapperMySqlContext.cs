using MySql.Data.MySqlClient;
using System.Data;

namespace Netricks_API.Context
{
    public class DapperMySqlContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperMySqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MySqlConnectionString");

        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }

}
