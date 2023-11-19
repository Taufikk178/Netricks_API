using Netricks_API.Entities.Register;

namespace Netricks_API.Interfaces
{
    public interface IRegisterDB
    {
        public Task<int> InsertUserDataIntoDB(UserRegisteration userRegisteration);

    }
}
