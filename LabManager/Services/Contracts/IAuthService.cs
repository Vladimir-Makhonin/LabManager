using LabManager.DTO.Auth;
using Microsoft.AspNetCore.Identity;

namespace LabManager.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(
            RegisterRequest request);

        Task<AuthResponse?> Login(
            LoginRequest request);
    }
}
