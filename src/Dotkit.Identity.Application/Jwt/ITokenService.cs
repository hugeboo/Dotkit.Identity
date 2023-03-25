using Dotkit.Identity.Application.Jwt;
using System.Security.Claims;

namespace Dotki.Identity.Application.Jwt
{
    internal interface ITokenService
    {
        Token GenerateToken(IEnumerable<Claim> claims);
    }
}
