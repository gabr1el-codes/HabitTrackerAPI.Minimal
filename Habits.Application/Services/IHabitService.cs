using Habits.Application.Models;
using Habits.Contracts.Requests;

namespace Habits.Application.Services;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetAllAsync(Guid userId, CancellationToken token = default);
    Task<Habit?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
    Task<bool> AddAsync(Guid userId, Habit habit, CancellationToken token = default);
    Task<Habit?> UpdateAsync(Guid id, Guid userId, Habit habit, CancellationToken token = default);
    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default);
}