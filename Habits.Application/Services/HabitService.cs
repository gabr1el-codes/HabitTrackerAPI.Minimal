using FluentValidation;
using Habits.Application.Models;
using Habits.Application.Repositories;
using Habits.Contracts.Requests;
using Microsoft.Extensions.Logging;

namespace Habits.Application.Services;

public class HabitService : IHabitService
{
    private readonly IHabitRepository _habitRepository;
    private readonly IValidator<Habit> _habitValidator;
    private readonly ILogger<HabitService> _logger;

    public HabitService(IHabitRepository habitRepository, IValidator<Habit> habitValidator, ILogger<HabitService> logger)
    {
        _habitRepository = habitRepository;
        _habitValidator = habitValidator;
        _logger = logger;
    }

    public async Task<bool> AddAsync(Guid userId, Habit habit, CancellationToken token = default)
    {
        _logger.LogInformation("Adding new habit: {HabitName}", habit.Name);
        await _habitValidator.ValidateAndThrowAsync(habit, token);
        return await _habitRepository.AddAsync(userId,habit, token);
    }

    public Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        _logger.LogInformation("Deleting habit with ID: {HabitId}", id);
        return _habitRepository.DeleteAsync(id, userId, token);
    }

    public Task<IEnumerable<Habit>> GetAllAsync(Guid userId, CancellationToken token = default)
    {
        _logger.LogInformation("Fetching all habits for user ID: { UserId}", userId);
        return _habitRepository.GetAllAsync(userId, token);
    }

    public Task<Habit?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        _logger.LogInformation("Fetching habit with ID: {HabitId}", id);
        return _habitRepository.GetByIdAsync(id, userId, token);
    }

    public async Task<Habit?> UpdateAsync(Guid id, Guid userId, Habit habit, CancellationToken token = default)
    {
        _logger.LogInformation("Updating habit with ID: {HabitId}", id);
        await _habitValidator.ValidateAndThrowAsync(habit, cancellationToken: token);
        return await _habitRepository.UpdateAsync(id, userId, habit, token);       
    }    
}