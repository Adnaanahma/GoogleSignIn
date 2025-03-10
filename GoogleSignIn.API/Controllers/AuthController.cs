using GoogleSignIn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleSignIn.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string redirectUrl)
        {
            // Delegate the login logic to the service
            await _authService.Login(redirectUrl);
            return Ok("Redirecting to Google login...");
        }

        [Authorize]
        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            // Delegate the logic to retrieve user information to the service
            var userInfo = _authService.GetUserInfo();
            return Ok(userInfo);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Delegate the logout logic to the service
            await _authService.Logout();
            return Ok("Logged out successfully.");
        }
    }
}
