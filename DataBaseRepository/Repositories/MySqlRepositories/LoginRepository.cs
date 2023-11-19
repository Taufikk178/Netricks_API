using Dapper;
using DataBaseRepository.DbContext;
using DataBaseRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Netricks.Core.Entities.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataBaseRepository.Repositories.MySqlRepositories
{
    public class LoginRepository : ILoginDB
    {
        //private readonly DapperContext _context;
        private readonly DapperMySqlContext _context;

        private readonly IConfiguration _config;

        public LoginRepository(DapperMySqlContext dapperContext, IConfiguration config)
        {
            _context = dapperContext;
            _config = config;
        }

        public async Task<bool> ValidateUsernamePassword(string username, string password)
        {
            bool result = false;

            ResponseAPI responseAPI = new ResponseAPI();

            try
            {


                var query = $"SELECT CASE WHEN EXISTS ( SELECT username, password FROM tbl_user WHERE USERNAME = '{username}' and password = '{password}' ) THEN 'true' ELSE 'false' END as Result";

                using (var connection = _context.GetConnection())
                {
                    var resultSet = await connection.QuerySingleOrDefaultAsync(query) ;

                    foreach (KeyValuePair<string, object> item in resultSet)
                    {
                        result = Convert.ToBoolean(item.Value);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}