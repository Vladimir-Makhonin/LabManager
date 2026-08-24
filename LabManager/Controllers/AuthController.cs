using LabManager.Contracts;
using LabManager.DTO.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            IdentityResult result =
                await _authService.Register(request);

            if (!result.Succeeded)
            {
                IEnumerable<string> errors =
                    result.Errors.Select(
                        error => error.Description);

                return BadRequest(errors);
            }

            return Ok(new
            {
                message = "User registered successfully."
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(
            LoginRequest request)
        {
            AuthResponse? response =
                await _authService.Login(request);

            if (response == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(response);
        }
    }
}