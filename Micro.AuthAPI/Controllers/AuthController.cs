using Micro.AuthAPI.Models;
using Micro.AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Micro.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {            
            var response = await _authService.Register(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDTO request)
        {
            var response = await _authService.Login(request);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.DisplayMessage);
        }

        [HttpGet, Authorize(Roles = "user,Admin")]
        public ActionResult<string> Aloha()
        {
            return Ok("Aloha! You're authorized!");
        }

    }
}
