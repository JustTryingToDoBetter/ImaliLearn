
using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Interfaces;
using ImaliLearn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImaliLearn.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context; // database context
    // constructor
    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    // check if budget exists
    public async Task<bool> ExistsAsync(Guid userId, int year, int month)
    {
        return await _context.Budgets.AnyAsync(b =>
            b.UserId == userId &&
            b.Year == year &&
            b.Month == month);
    }
    // add a new budget
    public async Task<Budget> CreateAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
        return budget;
    }
    // get budgets by user id
    public async Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.Year)
            .ThenByDescending(b => b.Month)
            .ToListAsync();
    }
}
