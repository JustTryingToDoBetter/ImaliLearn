using ImaliLearn.Application.Common.Results;
using ImaliLearn.Domain.Repositories;
using ImaliLearn.Domain.Entities;

namespace ImaliLearn.Application.Auth;

public class LoginUserService
{
    private readonly IUserRepository _users;
    private readonly PasswordHasher _hasher;
    private readonly JwtTokenService _jwt;

    private readonly IRefreshTokenRepository _refreshTokens;

    public LoginUserService(
        IUserRepository users,
        PasswordHasher hasher,
        JwtTokenService jwt,
        IRefreshTokenRepository refreshTokens)
    {
        _users = users;
        _hasher = hasher;
        _jwt = jwt;
        _refreshTokens = refreshTokens;
    }

    public async Task<Result<(string accessToken, string refreshToken)>> HandleAsync(
    string email, string password)
{
    var user = await _users.GetByEmailAsync(email);
    if (user == null || !_hasher.Verify(password, user.PasswordHash))
        return Result<(string, string)>.Failure("Invalid credentials.");

    var accessToken = _jwt.GenerateToken(user.Id, user.Email);
    var refreshTokenValue = _jwt.GenerateRefreshToken();

    var refreshToken = new RefreshToken
    {
        Id = Guid.NewGuid(),
        UserId = user.Id,
        Token = refreshTokenValue,
        ExpiresAt = DateTime.UtcNow.AddDays(7)
    };

    await _refreshTokens.AddAsync(refreshToken);

    return Result<(string, string)>.Success((accessToken, refreshTokenValue));
}
}