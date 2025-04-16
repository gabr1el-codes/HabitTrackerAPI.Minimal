using Habits.Application.Database;
using Habits.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Application.Repositories;
public class HabitRepository : IHabitRepository
{
    private readonly HabitTrackerDbContext _dbContext;

    public HabitRepository(HabitTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddAsync(Habit habit, CancellationToken token = default)
    {
        
        await _dbContext.Habits.AddAsync(habit, token); // Add the new habit to the database
        await _dbContext.SaveChangesAsync(token); // Save changes asynchronously to the database

        return true; // Return true to indicate the habit was successfully added
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
    {
        var habit = await _dbContext.Habits.FindAsync(id, token); // Find the habit by id

        if (habit == null)
        {
            return false; // Habit not found, return false
        }

        _dbContext.Habits.Remove(habit); // Remove the habit from the DbContext
        await _dbContext.SaveChangesAsync(token); // Commit changes to the database

        return true; // Return true if the habit was deleted successfully
    }

    public async Task<IEnumerable<Habit>> GetAllAsync(CancellationToken token = default)
    {
        return await _dbContext.Habits.ToListAsync(token); // Return all habits 
    }

    public async Task<Habit?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Habits.FindAsync(id, token); // returns the habit with the specified id or null if not found
    }

    /// Update the habit
    public async Task<Habit?> UpdateAsync(Guid id, Habit habit, CancellationToken token = default)
    {        
        var existingHabit = await _dbContext.Habits.FindAsync(id, token); // Find the habit by id

        if (existingHabit == null)        
            return null; // Return null if the habit does not exist

        // Update the properties of the existing habit
        existingHabit.Name = habit.Name;
        existingHabit.Description = habit.Description;


        if (!habit.IsCompleted)
        {
            existingHabit.CompletedAt = null; // Reset the completion date if the habit is marked as not completed
        }
        else if (habit.IsCompleted && !existingHabit.IsCompleted) //Just sets the time if the habit was not completed before
        {
            existingHabit.CompletedAt = DateTime.UtcNow; // Set the completion date if the habit is marked as completed
        }

        existingHabit.IsCompleted = habit.IsCompleted; // Update the completion status

        await _dbContext.SaveChangesAsync(token); // Save the changes to the database
        return existingHabit; // Return true if the update was successful
    }
}