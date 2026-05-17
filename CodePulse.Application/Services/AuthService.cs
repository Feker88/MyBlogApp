using CodePulse.Application.DTOs.Authentification;
using CodePulse.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CodePulse.Application.Services
{
    public class AuthService : IAuthServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager ,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async  Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            //fin user by mail
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return null;
            
            // Validate password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return null;

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);


            // Generate JWT token
            var token = GenerateJwtToken(user, roles);
            
            return new LoginResponseDto
            {
                Email = user.Email!,
                Token = token,
                Roles = roles
            };
        }

        /// <summary>
        ///  Register a new user with the provided information in the request. 
        ///  This method will create a new user in the database and return an IdentityResult indicating the success or failure of the registration process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterAsync(RegisterRequestDto request)
        {
            // Create new user
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            // UserManager handles password hashing automatically
            return await _userManager.CreateAsync(user, request.Password);
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            // ─── Claims ───
            // Claims are pieces of information about the user
            // embedded inside the token
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // ─── Signing credentials ───
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // ─── Build the token ───
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["Jwt:ExpiryInMinutes"]!)),
                signingCredentials: credentials
            );

            // ─── Serialize token to string ───
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
