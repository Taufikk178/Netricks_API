using Netricks_API.Entities.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Netricks_API.Interfaces
{
    public interface ILoginDB
    {
        //public Task<ResponseAPI> ValidateUsernamePassword(IConfiguration config, string username, string password);
        //public Task<ResponseAPI> GetUsernamePassword(string username, string password);
        public Task<bool> ValidateUsernamePassword(string username, string password);

    }

    public interface ILoginValidate
    {
        public Task<bool> ValidateUsernamePassword(string username, string password);

    }
}
