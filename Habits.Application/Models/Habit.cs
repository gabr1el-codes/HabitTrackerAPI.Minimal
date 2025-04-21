namespace Habits.Application.Models;

public class Habit
{    
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid UserID { get; set; }
}