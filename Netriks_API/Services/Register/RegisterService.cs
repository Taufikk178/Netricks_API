using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Netricks_API.Entities.Common;
using Netricks_API.Entities.Register;
using Netricks_API.Interfaces;
using Newtonsoft.Json;

namespace Netricks_API.Services.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterDB registerDB;
        public RegisterService(IRegisterDB registerDB)
        {
            this.registerDB = registerDB;
        }
        public async Task<ResponseAPI> InsertDataIntoDB(UserRegisteration userRegisteration)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                UserRegisterationValidator userRegisterationValidator = new UserRegisterationValidator();
                var validatedResult = userRegisterationValidator.Validate(userRegisteration);
                if (!validatedResult.IsValid)
                {
                    var ErrorList = new List<string>();
                    foreach (var item in validatedResult.Errors)
                    {
                        ErrorList.Add(item.ErrorMessage);
                        //responseAPI.Data += Convert.ToString(item.ErrorMessage) + "\n";

                    }
                    // await Console.Out.WriteLineAsync();
                    responseAPI.Code = 0;
                    responseAPI.Message = "Invalid Field";
                    responseAPI.Data = ErrorList;
                    return responseAPI;
                }
                int userId = await registerDB.InsertUserDataIntoDB(userRegisteration);
                if (userId > 0)
                {
                    responseAPI.Code = 1;
                    responseAPI.Message = "Success";
                    responseAPI.Data = userId;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return responseAPI;
        }
    }
}
