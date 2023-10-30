using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<AccountDto, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "Account/") : base(request)
        {
        }

        public async Task<ResponseOKHandler<TokenDto>> Login(LoginDto login)
        {
            string jsonEntity = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            try
            {
                using (var response = await httpClient.PostAsync($"{request}login", content))
                {
                    response.EnsureSuccessStatusCode();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
                    return entityVM;
                }
            }
            catch (Exception ex)
            {
                // Tangani kesalahan di sini, misalnya, log pesan kesalahan atau tampilkan pesan kesalahan kepada pengguna.
                Console.WriteLine(ex.Message);
                
                return null;
            }
        }
    }
}
