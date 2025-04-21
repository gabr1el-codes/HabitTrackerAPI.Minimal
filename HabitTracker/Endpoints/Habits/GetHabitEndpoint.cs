using Habits.Application.Services;
using HabitTracker.Auth;
using Habits.Contracts.Responses;
using HabitTracker.Mapping;

namespace HabitTracker.Endpoints.Habits;

public static class GetHabitEndpoint
{
    public const string Name = "GetHabit";
    public static IEndpointRouteBuilder MapGetHabit(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/habits/{id}", async (
            Guid id, 
            IHabitService habitService,
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var habit = await habitService.GetByIdAsync(id, userId.Value, token);
            if (habit is null)
            {
                return Results.NotFound();
            }

            var response = habit.MapToResponse();

            return TypedResults.Ok(response);
        })
            .WithName(Name)
            .Produces<HabitResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.MemberPolicyName);

        return app;
    }
}
