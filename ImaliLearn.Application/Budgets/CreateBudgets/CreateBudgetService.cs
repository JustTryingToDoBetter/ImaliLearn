using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Interfaces;
using ImaliLearn.Application.Common.Results;

namespace ImaliLearn.Application.Budgets.CreateBudgets;

public class CreateBudgetService
{
    private readonly IBudgetRepository _budgetRepository;

    public CreateBudgetService(IBudgetRepository budgetRepository)
    { 
        _budgetRepository = budgetRepository;
    }

    // Method to create a new budget asynchronously
    public async Task<Result<Guid>> HandleAsync(CreateBudgetCommand command)
    {
        if (command.Income < 0 || command.Expenses < 0 || command.SavingsGoal < 0)
            return Result<Guid>.Failure("Values must be non-negative.");

        // 1. enforce domain rules: one budget per user per month
        bool exists = await _budgetRepository.ExistsAsync(
            command.UserId,
            command.Year,
            command.Month);

        if (exists)
            return Result<Guid>.Failure("Budget already exists for this user and month.");


        // 2. create budget entity
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

        // 3. persist budget entity
        await _budgetRepository.CreateAsync(budget);

        //4. return budget id
        return Result<Guid>.Success(budget.Id);
    }

}