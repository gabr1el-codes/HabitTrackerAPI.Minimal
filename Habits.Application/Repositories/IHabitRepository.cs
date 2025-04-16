using Habits.Application.Models;

namespace Habits.Application.Repositories;

public interface IHabitRepository
{
    Task<IEnumerable<Habit>> GetAllAsync(CancellationToken token = default);
    Task<Habit?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<bool> AddAsync(Habit habit, CancellationToken token = default);
    Task<Habit?> UpdateAsync(Guid id, Habit habit, CancellationToken token = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}
