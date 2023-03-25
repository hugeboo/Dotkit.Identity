using Dotki.Identity.Application.Jwt;
using Dotkit.Identity.Application.Jwt;
using Dotkit.Identity.Persistence.Models;
using Dotkit.Identity.Persistence.Repository;
using Dotkit.Identity.Persistence.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Application.Auth
{
    internal sealed class AuthService : IAuthService
    {
        private readonly IIdentityRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenSrrvice;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IIdentityRepository repository, IPasswordHasher passwordHasher, ITokenService tokenService, 
            ILogger<AuthService> logger)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenSrrvice = tokenService;
            _logger = logger;
        }   

        public async Task<Token?> Login(string username, string password)
        {
            try
            {
                var user = await _repository.GetUserByName(username);
                if (user == null)
                {
                    _logger.LogDebug("Пользователь '{username}' не найден", username);
                    return null;
                }
                if (!user.IsActive)
                {
                    _logger.LogDebug("Пользователь '{username}' не активен", username);
                    return null;
                }
                if (!_passwordHasher.VerifyHashedPassword(user.Password, password))
                {
                    _logger.LogDebug("Неправильный пароль пользователя '{username}'", username);
                    return null;
                }
                var claims = await GetUserClaims(user);
                var token = _tokenSrrvice.GenerateToken(claims);
                user.RefreshToken = token.RefreshToken;
                await _repository.SaveChanges();
                return token;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ошибка авторизации пользователя '{username}'", username);
                throw;
            }
        }

        public async Task<Token?> RefreshAccessToken(string refreshToken)
        {
            try
            {
                var user = await _repository.GetUserByRefreshToken(refreshToken);
                if (user == null)
                {
                    return null;
                }
                var claims = await GetUserClaims(user);
                var token = _tokenSrrvice.GenerateToken(claims);
                user.RefreshToken = token.RefreshToken;
                await _repository.SaveChanges();
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка рефреша токена '{refreshToken}'", refreshToken);
                throw;
            }
        }

        private async Task<List<Claim>> GetUserClaims(User user)
        {
            var lst = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Dotkit.Id", user.Id.ToString()),
            };

            var lstUserClaim = await _repository.GetUserClaims(user.Id);
            lst.AddRange(lstUserClaim.Select(c => new Claim(c.CliamType, c.CliamValue ?? string.Empty)));

            return lst;
        } 
    }
}
