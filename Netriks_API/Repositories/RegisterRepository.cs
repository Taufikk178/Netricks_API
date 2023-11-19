using Dapper;
using Microsoft.AspNetCore.Connections;
using Netricks_API.Context;
using Netricks_API.Entities.Register;
using Netricks_API.Interfaces;

namespace Netricks_API.Repositories
{
    public class RegisterRepository : IRegisterDB
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _config;

        public RegisterRepository(DapperContext dapperContext, IConfiguration config)
        {
            _context = dapperContext;
            _config = config;
        }
        public async Task<int> InsertUserDataIntoDB(UserRegisteration userRegisteration)
        {
            try
            {
                using (var connection = _context.GetConnection())
                {
                    connection.Open();
                    var sql = "INSERT INTO tbl_user ( u_name, u_contact, u_email, username, password, u_state ) VALUES ( @u_name, @u_contact, @u_email, @username, @password, @u_state ); SELECT CAST(SCOPE_IDENTITY() as int)";
                    return await connection.QuerySingleAsync<int>(sql, userRegisteration);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
