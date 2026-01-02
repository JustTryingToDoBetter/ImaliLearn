using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Interfaces;
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

    public async Task<Budget> CreateAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
        return budget;
    }

    public async Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }
}
