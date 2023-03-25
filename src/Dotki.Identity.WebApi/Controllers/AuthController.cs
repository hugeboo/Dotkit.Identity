using Dotkit.Identity.Application.Auth;
using Dotkit.Identity.ExternalContracts;
using Dotkit.Identity.Persistence;
using Dotkit.Identity.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotki.Identity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Login(request.UserName, request.Password);
            if (token == null)
            {
                //return this.Unauthorized();
            }
            return this.Ok(new TokenResponse { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenRequest request)
        {
            var token = await _authService.RefreshAccessToken(request.RefreshToken);
            if (token == null)
            {
                return this.Unauthorized();
            }
            return this.Ok(new TokenResponse { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });
        }
    }
}