using FarmFresh.Core.Models.Requests;
using FarmFresh.Core.Models.Responses;
using FarmFresh.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService userService;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenService tokenService;

        public AuthController(IAuthService userService,
            ILogger<AuthController> logger,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var isAuthenticated = await userService.AuthenticateUserAsync(loginRequest);

                if (isAuthenticated is true)
                {
                    var claims = new[]
                    {
                        new Claim("Email", loginRequest.Email)
                    };

                    var (token, expiryMinutes) = tokenService.BuildToken(claims);

                    var loginResponse = new LoginResponse
                    {
                        Email = loginRequest.Email,
                        Token = token,
                        ExpiresAt = expiryMinutes
                    };

                    bool isValid = tokenService.IsValidToken(token);

                    if (isValid)
                    {
                        return Ok(loginResponse);
                    }
                }

                var result = new
                {
                    Result = "Unauthenticated."
                };

                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                var result = new
                {
                    Result = "Failed to authenticated user."
                };

                return BadRequest(result);
            }
        }
    }
}