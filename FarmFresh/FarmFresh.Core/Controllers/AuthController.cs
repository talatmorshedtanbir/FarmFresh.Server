using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenService tokenService;

        public AuthController(IUserService userService,
            ILogger<AuthController> logger,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var authenticationResponse = await userService.AuthenticateUser(loginRequestDto);

                if (authenticationResponse.Code is 1)
                {
                    var claims = new[]
                    {
                        new Claim("Email", loginRequestDto.EmailId)
                    };

                    var (token, expiryMinutes) = tokenService.BuildToken(claims);

                    var loginResponse = new LoginResponseDto
                    {
                        Email = loginRequestDto.EmailId,
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
                    Message = "Unauthenticated"
                };

                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                var result = new
                {
                    ex.Message
                };

                return BadRequest(result);
            }
        }
    }
}