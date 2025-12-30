
using ImaliLearn.Application.Common.Persistence;
using ImaliLearn.Application.Common.Results;
using ImaliLearn.Application.Budgets.CreateBudgets;


namespace ImaliLearn.Application.Budgets.CreateBudgets;


public class CreateBudgetServices
{
    private readonly IApplicationDbContext _context; // Database context for accessing application data
    // Constructor to initialize the service with the application database context
    public CreateBudgetServices(IApplicationDbContext context) : base(context)
    { 
        _context = context; // Assign the provided database context to the private field
    }

    // Method to create a new budget asynchronously
    public async CreateBudgetService Task<Result<Guid>> Handle(CreateBudgetCommand command)
    {
        var result = await _context.ExecuteAsync(async () =>
        {
            var budget = new Domain.Entities.Budget 
            {
                Name = command.Name,
                Description = command.Description,
                Income = command.Income,
                Expenses = command.Expenses,
                SavingsGoal = command.SavingsGoal,
                Year = command.Year,
                Month = command.Month
            };
            _context.Budgets.Add(budget); // Add the new budget to the database context
            await _context.SaveChangesAsync(); // Save changes to the database
            return budget.Id; // Return the ID of the newly created budget
        });
    }

}