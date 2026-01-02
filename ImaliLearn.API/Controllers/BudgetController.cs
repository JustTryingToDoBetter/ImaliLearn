// ==========================================
// 4️⃣ CONTROLLER USES USER ID FROM JWT
// ==========================================

// 📍 API/Controllers/BudgetsController.cs

using ImaliLearn.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ImaliLearn.Application.Budgets;
using ImaliLearn.API.Models;
using ImaliLearn.Application.Budgets.CreateBudgets;
using ImaliLearn.Application.Budgets.GetUserBudgets;

[Authorize(Policy = PolicyNames.MustBeAuthenticated)]
[ApiController]
[Route("api/budgets")]
public class BudgetsController : ControllerBase
{
    private readonly CreateBudgetService _create;
    private readonly GetUserBudgetsService _get;

    public BudgetsController(
        CreateBudgetService create,
        GetUserBudgetsService get)
    {
        _create = create;
        _get = get;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.GetUserId();

        var command = new CreateBudgetCommand
        {
            UserId = userId,
            Year = request.Year,
            Month = request.Month,
            Income = request.Income,
            Expenses = request.Expenses,
            SavingsGoal = request.SavingsGoal
        };

        var result = await _create.HandleAsync(command);

        return result.IsSuccess
            ? Created("", new { BudgetId = result.Value })
            : Conflict(new { error = result.Error });
    }

    [HttpGet]
    public async Task<IActionResult> GetMine()
    {
        var userId = User.GetUserId();
        var result = await _get.HandleAsync(userId);

        return Ok(result.Value);
    }
}
