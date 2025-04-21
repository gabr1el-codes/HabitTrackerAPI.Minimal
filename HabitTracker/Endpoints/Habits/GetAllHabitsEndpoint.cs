using Habits.Application.Services;
using HabitTracker.Auth;
using Habits.Contracts.Responses;
using HabitTracker.Mapping;

namespace HabitTracker.Endpoints.Habits;

public static class GetAllHabitsEndpoint
{
    public const string Name = "GetHabits";
    public static IEndpointRouteBuilder MapGetAllHabits(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/habits", async (
            IHabitService habitService,
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var habits = await habitService.GetAllAsync(userId.Value, token);

            var response = habits.MapToResponse();

            return TypedResults.Ok(response);
        })
            .WithName($"{Name}V1")
            .WithApiVersionSet(ApiVersioning.VersionSet)
            .HasApiVersion(1.0)
            .RequireAuthorization(AuthConstants.MemberPolicyName);

        app.MapGet("/api/habits", async (
            IHabitService habitService,
            HttpContext context,
            CancellationToken token) =>
        {
            var userId = context.GetUserId(); // Get the user ID from the context

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var habits = await habitService.GetAllAsync(userId.Value, token);

            var response = habits.MapToResponse();

            return TypedResults.Ok(response);
        })
            .WithName($"{Name}V2")
            .WithApiVersionSet(ApiVersioning.VersionSet)
            .HasApiVersion(2.0)
            .Produces<HabitsResponse>(StatusCodes.Status200OK)
            .RequireAuthorization(AuthConstants.MemberPolicyName);

        return app;
    }
}
