using Habits.Application.Services;
using Habits.Contracts.Auth;
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
            CancellationToken token) =>
        {    
            // Mapping the updated habit to a habit object using the version before the update
            var updatedHabit = request.MapToHabit(id);

            if (updatedHabit == null)
            {
                return Results.NotFound();
            }

            var habit = await habitService.UpdateAsync(id, updatedHabit, token);

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
