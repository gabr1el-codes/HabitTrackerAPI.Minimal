using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habits.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Application.Database;
public class HabitTrackerDbContext : DbContext
{
    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options)
    {
    }

    public DbSet<Habit> Habits => Set<Habit>(); // Table name is "Habits" by convention

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for the Habits table
        modelBuilder.Entity<Habit>().HasData(
             new Habit
             {
                 Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                 Name = "Drink water",
                 Description = "Drink 2L of water per day"
             },
             new Habit
             {
                 Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                 Name = "Exercise",
                 Description = "30 minutes of exercise per day"
             }
        );
    }
}
