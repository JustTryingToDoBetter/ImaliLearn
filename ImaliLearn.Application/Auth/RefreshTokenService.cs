using ImaliLearn.Application.Common.Results;
using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Auth;

public class RefreshTokenService
{
    private readonly IRefreshTokenRepository _tokens;
    private readonly IUserRepository _users;
    private readonly JwtTokenService _jwt;

    public RefreshTokenService(
        IRefreshTokenRepository tokens,
        IUserRepository users,
        JwtTokenService jwt)
    {
        _tokens = tokens;
        _users = users;
        _jwt = jwt;
    }

    public async Task<Result<string>> HandleAsync(string refreshToken)
    {
        var token = await _tokens.GetAsync(refreshToken);
        if (token == null || token.ExpiresAt < DateTime.UtcNow)
            return Result<string>.Failure("Invalid refresh token.");

        var user = await _users.GetByEmailAsync(
            _users.GetType().Name); // placeholder for lookup by ID

        var accessToken = _jwt.GenerateToken(token.UserId, user!.Email);
        return Result<string>.Success(accessToken);
    }
}