using Habits.Application.Services;
using HabitTracker.Auth;
using Habits.Contracts.Requests;
using Habits.Contracts.Responses;
using HabitTracker.Mapping;
using Habits.Application.Models;

namespace HabitTracker.Endpoints.Habits;

public static class CreateHabitEndpoint
{
    public const string Name = "CreateHabit";
    public static IEndpointRouteBuilder MapCreateHabit(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/habits", async (
            CreateHabitRequest request, 
            IHabitService habitService,
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context  

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var habit = request.MapToHabit(); // Map the request to a Habit object

            await habitService.AddAsync(userId.Value, habit, token); // Add the habit to the service

            var response = habit.MapToResponse(); // Map the habit to a response object

            return TypedResults.CreatedAtRoute(response, GetHabitEndpoint.Name, new { id = habit.Id }); // Return a 201 Created response with the location of the new resource

        })
            .WithName(Name)
            .WithApiVersionSet(ApiVersioning.VersionSet)
            .Produces<HabitResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(AuthConstants.MemberPolicyName);

        return app;
    }
}
