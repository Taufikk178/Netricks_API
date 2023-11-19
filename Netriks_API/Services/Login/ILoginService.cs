using Netricks_API.Entities.Common;

namespace Netricks_API.Services.Login
{
    public interface ILoginService
    {
        public Task<ResponseAPI> ValidateUsernamePassword(string username, string password);

    }
}
