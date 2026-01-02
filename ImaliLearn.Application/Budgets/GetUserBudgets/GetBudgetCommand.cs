

namespace ImaliLearn.Application.Budget.GetUserBudgets;

public class GetUserBudget
{
    public Guid BudgetId {get; init;}

    public int Year { get; init;}

    public int Month { get; init;}

    public decimal  Income { get; init;}

    public decimal Expenses { get; init;}

    public decimal SavingsGoal { get; init;}
    
}