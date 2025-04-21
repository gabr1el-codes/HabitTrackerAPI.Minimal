using Habits.Application.Services;
using HabitTracker.Auth;
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
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context  

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var delete = await habitService.DeleteAsync(id, userId.Value, token);
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