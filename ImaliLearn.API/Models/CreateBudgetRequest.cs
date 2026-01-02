using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImaliLearn.API.Models;

public class CreateBudgetRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Range(2000, 2100)]
    public int Year { get; set; }

    [Range(1, 12)]
    public int Month { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Income { get; set; }
    
    [Range(0, double.MaxValue)]
    public decimal Expenses { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SavingsGoal { get; set; }
}
