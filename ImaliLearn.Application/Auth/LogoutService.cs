using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Auth;

public class LogoutService
{
    private readonly IRefreshTokenRepository _tokens;

    public LogoutService(IRefreshTokenRepository tokens)
    {
        _tokens = tokens;
    }

    public async Task HandleAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return;

        await _tokens.RevokeAsync(refreshToken);
    }
}
