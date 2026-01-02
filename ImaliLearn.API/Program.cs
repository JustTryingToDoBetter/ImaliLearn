using ImaliLearn.Application.Budgets.CreateBudgets;
using ImaliLearn.Infrastructure;
using ImaliLearn.Application.Budgets.GetUserBudgets;
using ImaliLearn.Infrastructure.Persistence;
using ImaliLearn.API.Middleware;
using ImaliLearn.API.Models;
using Microsoft.AspNetCore.Mvc;
using ImaliLearn.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using JwtSettings = ImaliLearn.Domain.Configuration.JwtSettings;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CreateBudgetService>();
builder.Services.AddScoped<GetUserBudgetsService>();
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Customize the response for invalid model state
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value!.Errors.Count > 0) // Filter to only those with errors 
                .ToDictionary(
                    k => k.Key,
                    v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray() // Project error messages
                );

            var response = new ErrorResponse
            {
                Code = "validation_error",
                Message = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                TraceId = context.HttpContext.TraceIdentifier,
                Details = errors
            };

            return new BadRequestObjectResult(response);
        };
    });

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var settings = builder.Configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>()!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(settings.SecretKey))
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();

// Seed database
await DatabaseSeeder.SeedRolesAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
