using ImaliLearn.API.Models;
using ImaliLearn.Application.Budgets.CreateBudgets;
using ImaliLearn.Application.Budgets.GetUserBudgets;
using Microsoft.AspNetCore.Mvc;

namespace ImaliLearn.API.Controllers;

[ApiController]
[Route("api/budgets")]
public class BudgetController : ControllerBase
{
    private readonly CreateBudgetService _createBudgetService;
    private readonly GetUserBudgetsService _getUserBudgetsService;

    public BudgetController(
        CreateBudgetService createBudgetService,
        GetUserBudgetsService getUserBudgetsService)
    {
        _createBudgetService = createBudgetService;
        _getUserBudgetsService = getUserBudgetsService;
    }

    // POST /api/budgets
   [HttpPost]
public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var command = new CreateBudgetCommand
    {
        UserId = request.UserId,
        Year = request.Year,
        Month = request.Month,
        Income = request.Income,
        Expenses = request.Expenses,
        SavingsGoal = request.SavingsGoal
    };

    var result = await _createBudgetService.HandleAsync(command);

    if (!result.IsSuccess)
        return Conflict(new { error = result.Error });

    return CreatedAtAction(
        nameof(GetUserBudgets),
        new { userId = request.UserId },
        new { BudgetId = result.Value });
}

[HttpGet("user/{userId}")]
public async Task<IActionResult> GetUserBudgets(Guid userId)
{
    var result = await _getUserBudgetsService.HandleAsync(userId);
    return Ok(result.Value);
}

}
