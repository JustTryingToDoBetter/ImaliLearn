using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Repositories;
using ImaliLearn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImaliLearn.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid userId, int year, int month)
    {
        return await _context.Budgets.AnyAsync(b =>
            b.UserId == userId &&
            b.Year == year &&
            b.Month == month);
    }

    public async Task AddAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Budget>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.Year)
            .ThenByDescending(b => b.Month)
            .ToListAsync();
    }
}
