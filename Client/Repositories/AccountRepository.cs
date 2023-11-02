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

        public async Task<ResponseOKHandler<ClaimsDto>> Claims(string tokenDto)
        {
            string jsonEntity = JsonConvert.SerializeObject(tokenDto);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync($"{request}GetClaims/" + tokenDto))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ClaimsDto>>(apiResponse);
                return entityVM;
            }
        }

        public async Task<ResponseOKHandler<TokenDto>> Login(LoginDto login)
        {
            string jsonEntity = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}login", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
                return entityVM;
            }
        }
    }
}
