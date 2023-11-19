using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperGeneric
{
    public class DapperMsSqlContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperMsSqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection GetConnection()
            => new SqlConnection(_connectionString);
    }
}