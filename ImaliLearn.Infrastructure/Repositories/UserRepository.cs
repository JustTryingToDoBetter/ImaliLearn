using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Repositories;
using ImaliLearn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImaliLearn.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (applicationUser == null)
            return null;
        
        return new User 
        { 
            Id = Guid.Parse(applicationUser.Id),
            Email = applicationUser.Email ?? string.Empty
        };
    }

    public async Task AddAsync(User user)
    {
        var applicationUser = new User
        {
            Id = user.Id.ToString(),
            Email = user.Email
        };
        _context.Users.Add(applicationUser);
        await _context.SaveChangesAsync();
    }
}