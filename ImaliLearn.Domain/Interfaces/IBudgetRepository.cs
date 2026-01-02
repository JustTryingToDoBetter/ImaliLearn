using ImaliLearn.Domain.Entities;

namespace ImaliLearn.Domain.Interfaces;

public interface IBudgetRepository
{
    Task<bool> ExistsAsync(Guid userId, int year, int month);
    Task<Budget> CreateAsync(Budget budget);
    Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(Guid userId);
}
