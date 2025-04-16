using FluentValidation;
using Habits.Application.Models;

public class HabitValidator : AbstractValidator<Habit>
{
    public HabitValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        // Description can't be null but can be empty
        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage("Description cannot be null, altough it can be empty.");

        RuleFor(x => x.IsCompleted)
            .NotNull()
            .WithMessage("IsCompleted is required.");
    }
}