namespace ImaliLearn.Domain.Entities;

public class Budget
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public decimal Income { get; set; }

    public decimal Expenses { get; set; }

    public decimal SavingsGoal { get; set; }

    public DateTime CreatedAt { get; set; }
}
