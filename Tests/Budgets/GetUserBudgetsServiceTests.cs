using System;
using System.Threading.Tasks;
using ImaliLearn.Application.Budgets.GetUserBudgets;
using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Repositories;
using ImaliLearn.Infrastructure.Repositories;
using ImaliLearn.Tests.Helpers;
using Xunit;

public class GetUserBudgetsServiceTests
{
    [Fact]
    public async Task Returns_budgets_for_user_only()
    {
        var context = TestDbContextFactory.Create();
        var repo = new BudgetRepository(context);
        var service = new GetUserBudgetsService(repo);

        var userId = Guid.NewGuid();

        context.Budgets.AddRange(
            new Budget { Id = Guid.NewGuid(), UserId = userId, Year = 2026, Month = 1 },
            new Budget { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Year = 2026, Month = 2 }
        );

        await context.SaveChangesAsync();

        var result = await service.HandleAsync(userId);

        Assert.Single(result.Value!);
    }
}