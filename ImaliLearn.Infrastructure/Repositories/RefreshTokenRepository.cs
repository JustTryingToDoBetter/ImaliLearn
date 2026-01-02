using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Repositories;
using ImaliLearn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImaliLearn.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == token && !t.IsRevoked);
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        token.IsRevoked = true;
        await _context.SaveChangesAsync();
    }
}