using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Auth;

public class LogoutAllSessionsService
{
    private readonly IRefreshTokenRepository _tokens;

    public LogoutAllSessionsService(IRefreshTokenRepository tokens)
    {
        _tokens = tokens;
    }

    public async Task HandleAsync(Guid userId)
    {
        await _tokens.RevokeAllForUserAsync(userId);
    }
}