using Habits.Application.Models;
using Habits.Contracts.Requests;

namespace Habits.Application.Services;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetAllAsync(CancellationToken token = default);
    Task<Habit?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<bool> AddAsync(Habit habit, CancellationToken token = default);
    Task<Habit?> UpdateAsync(Guid id, Habit habit, CancellationToken token = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}