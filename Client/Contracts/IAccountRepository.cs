using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<AccountDto, Guid>
    {
        Task<ResponseOKHandler<ClaimsDto>> Claims(string tokenDto);
        Task<ResponseOKHandler<TokenDto>> Login(LoginDto login);
    }
}
