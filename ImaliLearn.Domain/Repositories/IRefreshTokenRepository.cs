using ImaliLearn.Domain.Entities;

namespace ImaliLearn.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetAsync(string token); // get token by token string
    Task RevokeAsync(RefreshToken token); // revoke a refresh token
}