using Habits.Application.Models;
using Habits.Contracts.Responses;
using Habits.Contracts.Requests;

namespace HabitTracker.Mapping;

public static class ContractMapping
{
    public static HabitResponse MapToResponse(this Habit habit)
    {
        return new HabitResponse
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            IsCompleted = habit.IsCompleted,
            CompletedAt = habit.CompletedAt
        };
    }

    public static HabitsResponse MapToResponse(this IEnumerable<Habit> habits)
    {
        return new HabitsResponse
        {
            Items = habits.Select(MapToResponse) // Map each habit to its response model
        };
    }

    public static Habit MapToHabit(this CreateHabitRequest habit)
    {
        return new Habit
        {
            Id = Guid.NewGuid(), // Generate a new ID for the habit
            Name = habit.Name,
            Description = habit.Description ?? string.Empty, // Default to empty string if null
            IsCompleted = false, // Default value for new habits
            CompletedAt = null // Default value for new habits
        };
    }

    public static Habit MapToHabit(this UpdateHabitRequest request, Guid id)
    {
        return new Habit
        {
            Id = id, // Use the provided ID
            Name = request.Name,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        };
    }   
}