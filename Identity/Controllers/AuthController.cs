using Identity.Domain.Dtos;
using Identity.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _tokenService.GenerateJwtToken(model);
            return string.IsNullOrEmpty(loginResult.Token) ? Unauthorized() : Ok(loginResult);
        }
    }
}

