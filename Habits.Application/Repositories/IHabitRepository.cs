using Habits.Application.Models;

namespace Habits.Application.Repositories;

public interface IHabitRepository
{
    Task<IEnumerable<Habit>> GetAllAsync(Guid userId, CancellationToken token = default);
    Task<Habit?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
    Task<bool> AddAsync(Guid userid, Habit habit, CancellationToken token = default);
    Task<Habit?> UpdateAsync(Guid id, Guid userid, Habit habit, CancellationToken token = default);
    Task<bool> DeleteAsync(Guid id, Guid userid, CancellationToken token = default);
}
