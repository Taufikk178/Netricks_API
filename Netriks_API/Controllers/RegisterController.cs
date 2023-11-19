using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netricks_API.Entities.Login;
using Netricks_API.Entities.Register;
using Netricks_API.Services.Register;

namespace Netricks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;
        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisteration userRegisteration)
        {
            try
            {
                return Ok(await registerService.InsertDataIntoDB(userRegisteration));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
