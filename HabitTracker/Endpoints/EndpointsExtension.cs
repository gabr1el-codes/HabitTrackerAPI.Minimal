using HabitTracker.Endpoints.Habits;

namespace HabitTracker.Endpoints;

public static class EndpointsExtension
{    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapMoviesEndpoints();
        return app;
    }
}
