using System.Net.Http.Json;

namespace ImaliLearn.Web.Services;

public class BudgetService
{
    private readonly HttpClient _http;

    public BudgetService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BudgetDto>> GetBudgetsAsync()
    {
        var response = await _http.GetFromJsonAsync<List<BudgetDto>>("api/budgets");
        return response ?? new List<BudgetDto>();
    }

    public async Task<BudgetDto?> GetBudgetAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<BudgetDto>($"api/budgets/{id}");
    }

    public async Task<(bool Success, string? Error)> CreateBudgetAsync(CreateBudgetRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/budgets", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (false, error?.Error ?? "Failed to create budget");
        }

        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteBudgetAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"api/budgets/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return (false, "Failed to delete budget");
        }

        return (true, null);
    }
}

public record BudgetDto(
    Guid Id,
    string Name,
    decimal TotalAmount,
    decimal SpentAmount,
    DateTime StartDate,
    DateTime EndDate,
    string? Description);

public record CreateBudgetRequest(
    string Name,
    decimal TotalAmount,
    DateTime StartDate,
    DateTime EndDate,
    string? Description);

public record ErrorResponse(string? Error);
