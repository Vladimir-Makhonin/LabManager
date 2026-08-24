using LabManager.Contracts;
using LabManager.DTO.Auth;
using LabManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LabManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> Register(
            RegisterRequest request)
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                Email = request.Email
            };

            IdentityResult result =
                await _userManager.CreateAsync(
                    user,
                    request.Password!);

            return result;
        }

        public async Task<AuthResponse?> Login(
            LoginRequest request)
        {
            ApplicationUser? user =
                await _userManager.FindByEmailAsync(
                    request.Email!);

            if (user == null)
            {
                return null;
            }

            bool passwordIsValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    request.Password!);

            if (!passwordIsValid)
            {
                return null;
            }

            return await CreateTokenAsync(user);
        }

        private async Task<AuthResponse> CreateTokenAsync(
            ApplicationUser user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>(
                    "Jwt:ExpirationMinutes"));

            List<Claim> claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email!),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };

            IList<string> roles =
                await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role));
            }

            string jwtKey =
                _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "The JWT key is not configured.");

            SymmetricSecurityKey securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey));

            SigningCredentials signingCredentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token =
                new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: signingCredentials);

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),

                Expiration = expiration
            };
        }
    }
}
