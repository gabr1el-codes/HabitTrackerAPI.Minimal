using Habits.Application.Services;
using Habits.Contracts.Auth;
using Habits.Contracts.Requests;
using Habits.Contracts.Responses;
using HabitTracker.Mapping;

namespace HabitTracker.Endpoints.Habits;

public static class CreateHabitEndpoint
{
    public const string Name = "CreateHabit";
    public static IEndpointRouteBuilder MapCreateHabit(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/habits", async (
            CreateHabitRequest request, 
            IHabitService habitService, 
            CancellationToken token) =>
        {
            var habit = request.MapToHabit(); // Map the request to a Habit object

            await habitService.AddAsync(habit, token); // Add the habit to the service

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
