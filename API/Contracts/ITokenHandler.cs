using API.DTOs.Accounts;
using System.Security.Claims;

namespace API.Contracts
{
    public interface ITokenHandler
    {
        // Defines method to generates a JWT token based on the specified claims
        string Generate(IEnumerable<Claim> claims);

        // Defines method to extract claims form JWT token
        ClaimsDto ExtractClaimsFromJwt(string token);
    }
}
