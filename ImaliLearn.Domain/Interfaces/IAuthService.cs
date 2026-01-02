using ImaliLearn.Domain.DTOs.Auth;

namespace ImaliLearn.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
