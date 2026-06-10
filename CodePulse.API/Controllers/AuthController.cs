using CodePulse.Application.DTOs.Authentification;
using CodePulse.Application.Interfaces;
using CodePulse.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices ?? throw new ArgumentNullException(nameof(AuthService));
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Validate request
            //Return  defautl 400 BadRequest 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _authServices.RegisterAsync(request);

            if (!result.Succeeded)
            {
                // Return all Identity errors(e.g.password too weak, email taken)
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "Registration failed", errors });
            }
            //return 200
            return Ok(new { Message = "User registered successfully." });

        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authServices.LoginAsync(request);
            if (response is null)
                //retrun 401 
                return Unauthorized(new { message = "Invalid email or password." });

            //return 200
             return Ok(response);
        }

    }
}
