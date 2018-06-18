using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Core.Auth;

namespace AuthService.Helpers
{
    public class Tokens
    {
        public static async Task<Object> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, TokenUserModel user, JwtIssuerOptions jwtOptions)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id).Value,
                auth_token = await jwtFactory.GenerateEncodedToken(user, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return response;
        }
    }
}
