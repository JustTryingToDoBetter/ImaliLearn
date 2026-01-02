// ==========================================
// 4️⃣ UPDATE GET USER BUDGETS USE CASE
// ==========================================

// 📍 Application/Budgets/GetUserBudgets/GetUserBudgetsService.cs

using ImaliLearn.Application.Common.Results;
using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Budgets.GetUserBudgets;

public class GetUserBudgetsService
{
    private readonly IBudgetRepository _repository;

    public GetUserBudgetsService(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<BudgetSummary>>> HandleAsync(Guid userId)
    {
        var budgets = await _repository.GetByUserIdAsync(userId);

        var summaries = budgets.Select(b => new BudgetSummary
        {
            BudgetId = b.Id,
            Year = b.Year,
            Month = b.Month,
            Income = b.Income,
            Expenses = b.Expenses,
            SavingsGoal = b.SavingsGoal
        }).ToList();

        return Result<IReadOnlyList<BudgetSummary>>.Success(summaries);
    }
}
