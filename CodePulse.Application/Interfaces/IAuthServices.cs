using CodePulse.Application.DTOs.Authentification;
using Microsoft.AspNetCore.Identity;

namespace CodePulse.Application.Interfaces
{
    public  interface IAuthServices
    {

        /// <summary>
        /// Registers a new user with the given credentials.
        /// </summary>
        /// <param name="request">The registration details including email and password.</param>
        /// <returns>IdentityResult indicating success or failure with error details.</returns>
        Task<IdentityResult> RegisterAsync(RegisterRequestDto request);

        /// <summary>
        /// Validates user credentials and returns a JWT token on success.
        /// </summary>
        /// <param name="request">The login details including email and password.</param>
        /// <returns>LoginResponseDto with token and roles if successful; otherwise null.</returns>
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
