namespace ImaliLearn.Application.Budgets.GetUserBudgets;

public class BudgetSummary
{
    public Guid BudgetId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal SavingsGoal { get; set; }
}
