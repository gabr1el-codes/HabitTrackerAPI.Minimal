namespace HabitTracker.Endpoints.Habits;

public static class HabitsEndpointExtension
{
    public static IEndpointRouteBuilder MapMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetHabit();
        app.MapGetAllHabits();
        app.MapCreateHabit();
        app.MapUpdateHabit();
        app.MapDeleteHabit();
        return app;
    }
}
