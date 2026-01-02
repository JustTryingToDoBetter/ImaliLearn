using ImaliLearn.Domain.Interfaces;
using ImaliLearn.Application.Common.Results;

namespace ImaliLearn.Application.Budgets.GetUserBudgets;

public class GetUserBudgetsService
{
    private readonly IBudgetRepository _budgetRepository;
    public GetUserBudgetsService(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }
    // accept userId
    public async Task<Result<IReadOnlyList<BudgetSummary>>> HandleAsync(Guid userId)
    {
        
        //query budgets belong to user
        var budgets = await _budgetRepository.GetBudgetsByUserIdAsync(userId);
        // order them
        var orderedBudgets = budgets.OrderBy(b => b.Year).ThenBy(b => b.Month);
        // project into read model
        var budgetSummaries = orderedBudgets.Select(b => new BudgetSummary
        {
            BudgetId = b.Id,
            Year = b.Year,
            Month = b.Month,
            Income = b.Income,
            Expenses = b.Expenses,
            SavingsGoal = b.SavingsGoal
        }).ToList();
        // return to collection of read model
        return Result<IReadOnlyList<BudgetSummary>>.Success(budgetSummaries);
    }
    
}