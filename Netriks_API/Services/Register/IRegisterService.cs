using Netricks_API.Entities.Common;
using Netricks_API.Entities.Register;

namespace Netricks_API.Services.Register
{
    public interface IRegisterService
    {
        public Task<ResponseAPI> InsertDataIntoDB(UserRegisteration userRegisteration);

    }
}
