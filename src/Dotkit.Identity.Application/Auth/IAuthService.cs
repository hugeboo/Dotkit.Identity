using Dotkit.Identity.Application.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Application.Auth
{
    public interface IAuthService
    {
        Task<Token?> Login(string username, string password);
        Task<Token?> RefreshAccessToken(string refreshToken);
    }
}
