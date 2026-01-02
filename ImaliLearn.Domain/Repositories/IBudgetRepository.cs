using ImaliLearn.Domain.Entities;

namespace ImaliLearn.Domain.Repositories;

public interface IBudgetRepository
{
    Task<bool> ExistsAsync(Guid userId, int year, int month); // check if budget exists
    Task AddAsync(Budget budget); // add a new budget
    Task<IReadOnlyList<Budget>> GetByUserIdAsync(Guid userId); // get budgets by user id
}