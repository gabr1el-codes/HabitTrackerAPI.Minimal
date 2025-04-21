using Habits.Application.Services;
using HabitTracker.Auth;
using Habits.Contracts.Requests;
using Habits.Contracts.Responses;
using HabitTracker.Mapping;

namespace HabitTracker.Endpoints.Habits;

public static class UpdateHabitEndpoint
{
    public const string Name = "UpdateHabit";
    public static IEndpointRouteBuilder MapUpdateHabit(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/habits/{id}", async (
            Guid id,
            UpdateHabitRequest request,
            IHabitService habitService,
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            // Mapping the updated habit to a habit object using the version before the update
            var updatedHabit = request.MapToHabit(id);

            if (updatedHabit == null)
            {
                return Results.NotFound();
            }

            // Update the habit using the habit service
            var habit = await habitService.UpdateAsync(id, userId.Value, updatedHabit, token);

            if (habit == null)
            {
                return Results.NotFound();
            }


            var response = habit.MapToResponse(); // Map the habit to a response object
            return TypedResults.Ok(response); // Return a 200 OK response with the updated habit
        })
        .WithName(Name)
        .Produces<HabitResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
        .RequireAuthorization(AuthConstants.MemberPolicyName);

        return app;
    }
}
