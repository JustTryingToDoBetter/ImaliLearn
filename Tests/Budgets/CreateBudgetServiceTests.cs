using System;
using System.Threading.Tasks;
using ImaliLearn.Application.Budgets.CreateBudgets;
using ImaliLearn.Infrastructure.Repositories;
using ImaliLearn.Tests.Helpers;
using Xunit;

public class CreateBudgetServiceTests
{
    [Fact]
    public async Task Creates_budget_successfully()
    {
        var context = TestDbContextFactory.Create();
        var repo = new BudgetRepository(context);
        var service = new CreateBudgetService(repo);

        var command = new CreateBudgetCommand
        {
            UserId = Guid.NewGuid(),
            Year = 2026,
            Month = 1,
            Income = 10000,
            Expenses = 5000,
            SavingsGoal = 2000
        };

        var result = await service.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }

    [Fact]
    public async Task Prevents_duplicate_budget_for_same_month()
    {
        var context = TestDbContextFactory.Create();
        var repo = new BudgetRepository(context);
        var service = new CreateBudgetService(repo);

        var userId = Guid.NewGuid();

        var command = new CreateBudgetCommand
        {
            UserId = userId,
            Year = 2026,
            Month = 1,
            Income = 8000,
            Expenses = 4000,
            SavingsGoal = 1000
        };

        await service.HandleAsync(command);
        var result = await service.HandleAsync(command);

        Assert.False(result.IsSuccess);
    }
}