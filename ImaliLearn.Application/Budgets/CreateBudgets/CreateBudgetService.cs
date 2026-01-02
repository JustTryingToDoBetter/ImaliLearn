// ==========================================
// 3️⃣ UPDATE CREATE BUDGET USE CASE
// ==========================================

// 📍 Application/Budgets/CreateBudgets/CreateBudgetService.cs

using ImaliLearn.Application.Common.Results;

using ImaliLearn.Domain.Interfaces;

namespace ImaliLearn.Application.Budgets.CreateBudgets;

public class CreateBudgetService
{
    private readonly IBudgetRepository _repository;

    public CreateBudgetService(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> HandleAsync(CreateBudgetCommand command)
    {
        if (command.Income < 0 || command.Expenses < 0 || command.SavingsGoal < 0)
            return Result<Guid>.Failure("Values must be non-negative.");

        if (await _repository.ExistsAsync(command.UserId, command.Year, command.Month))
            return Result<Guid>.Failure("Budget already exists for this user and month.");

        var budget = new Domain.Entities.Budget
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Year = command.Year,
            Month = command.Month,
            Income = command.Income,
            Expenses = command.Expenses,
            SavingsGoal = command.SavingsGoal,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(budget);

        return Result<Guid>.Success(budget.Id);
    }
}
