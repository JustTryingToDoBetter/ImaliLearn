


namespace ImaliLearn.Application.Budgets.CreateBudgets;

public class CreateBudgetCommand : IRequest<Result<Guid>> // Command to create a new budget
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Income { get; set; } = 0;
    public decimal Expenses { get; set; } = 0;
    public decimal SavingsGoal { get; set; } = 0;
    public int Year { get; set; }
    public int Month { get; set; }
}