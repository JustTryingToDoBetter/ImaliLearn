using ImaliLearn.API.Security;
using ImaliLearn.API.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ImaliLearn.API.Security.Handlers;

public class BudgetOwnerHandler : AuthorizationHandler<BudgetOwnerRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        BudgetOwnerRequirement requirement)
    {
        var userIdClaim =
            context.User.FindFirst(ClaimTypes.NameIdentifier) ??
            context.User.FindFirst("sub");

        if (userIdClaim != null)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}