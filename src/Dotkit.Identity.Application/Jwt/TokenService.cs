using Dotkit.Identity.Application.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Dotki.Identity.Application.Jwt
{
    internal sealed class TokenService : ITokenService, IDisposable
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public TokenService(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public Token GenerateToken(IEnumerable<Claim> claims)
        {
            return new Token() 
            {
                AccessToken = GenerateAccessToken(claims), 
                RefreshToken = GenerateRefreshToken() 
            };
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            JwtSecurityToken accessToken = new(
                expires: DateTime.Now.AddMinutes(_jwtConfiguration.LifetimeInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(_jwtConfiguration.Secret, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            _rng.GetBytes(randomNumber);
            string token = Convert.ToBase64String(randomNumber);
            return token;
        }

        public void Dispose()
        {
            _rng.Dispose();
        }
    }
}
