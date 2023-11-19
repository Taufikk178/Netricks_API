using Microsoft.IdentityModel.Tokens;
using Netricks_API.Entities.Common;
using Netricks_API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Netricks_API.Services.Login
{
    public class LoginService : ILoginService
    {
        private ILoginDB _login;
        private IConfiguration _config;


        public LoginService(ILoginDB login, IConfiguration config)
        {
            _login = login;
            _config = config;

        }
        public async Task<ResponseAPI> ValidateUsernamePassword(string username, string password)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                bool resultFlag = await _login.ValidateUsernamePassword(username, password);
                if (resultFlag)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: _config["Jwt:Issuer"],
                        audience: _config["Jwt:Audience"],
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    if (!string.IsNullOrEmpty(tokenString))
                    {
                        responseAPI.Code = 1;
                        responseAPI.Message = "Success";
                        responseAPI.Data = tokenString;
                    }
                }
                else
                {
                    responseAPI.Code = 0;
                    responseAPI.Message = "Invalid Username or Password";
                    responseAPI.Data = null;
                }
                return responseAPI;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
