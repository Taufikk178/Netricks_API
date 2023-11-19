using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Netricks_API.Entities.Common;
using Netricks_API.Entities.Login;
using Netricks_API.Interfaces;
using Netricks_API.Services.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Netricks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService loginService;

        public LoginController(ILoginService login)
        {
            loginService = login;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            //Response response = new Response();

            if (loginRequest.Password is null || loginRequest.Username is null)
            {
                return BadRequest("Invalid client request body data");
            }
            try
            {
                var response = await loginService.ValidateUsernamePassword(loginRequest.Username, loginRequest.Password);

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}