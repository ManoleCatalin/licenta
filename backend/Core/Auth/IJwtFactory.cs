using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Data.Core.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(TokenUserModel user, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, Guid id);
    }
}
