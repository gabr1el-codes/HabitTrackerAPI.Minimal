using Habits.Application.Services;
using Habits.Contracts.Auth;
using HabitTracker.Mapping;

namespace HabitTracker.Endpoints.Habits;

public static class DeleteHabitEndpoint
{
    public const string Name = "DeleteHabit";

    public static IEndpointRouteBuilder MapDeleteHabit(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/habits/{id}", async (
            Guid id, 
            IHabitService habitService, 
            CancellationToken token) =>
        {
            var delete = await habitService.DeleteAsync(id, token);
            if (!delete)
            {
                return Results.NotFound();
            }
            return Results.Ok();
        })
        .WithName(Name)
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization(AuthConstants.MemberPolicyName);

        return app;
    }
}