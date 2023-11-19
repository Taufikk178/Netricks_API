
using Netricks.Core.Entities.Register;

namespace DataBaseRepository.Interfaces
{
    public interface IRegisterDB
    {
        public Task<int> InsertUserDataIntoDB(UserRegisteration userRegisteration);

    }
}
