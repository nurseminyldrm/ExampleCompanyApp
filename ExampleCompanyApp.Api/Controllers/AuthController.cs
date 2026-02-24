using ExampleCompanyApp.Api.AuthenticationService;
using ExampleCompanyApp.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCompanyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if(loginDto.Email == "test@example.com" && loginDto.Password == "123")
            {
                var token = _jwtService.GenerateToken(loginDto.Email, "Admin");
                return Ok(new {Token = token});
            }
            return Unauthorized();
        }
    }
}
