using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Habits.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Application.Database;
public class HabitTrackerDbContext : DbContext
{
    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options)
    {
    }

    public DbSet<Habit> Habits => Set<Habit>(); // Table name is "Habits" by convention
    public DbSet<User> Users => Set<User>(); // Table name is "Users" by convention

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var userId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var hashedPassword = "$2a$11$GZ.ursK2W8E3HD357j4hFOoC8SdU/saarJaKsBR6dJm/K6a8l5FfK";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = userId,
                Email = "user@example.com",
                PasswordHash = hashedPassword
            }
        );

        // Seed data for the Habits table
        modelBuilder.Entity<Habit>().HasData(
             new Habit
             {
                 Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                 Name = "Drink water",
                 Description = "Drink 2L of water per day",
                 UserID = userId // Set the UserID to the seeded user
             },
             new Habit
             {
                 Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                 Name = "Exercise",
                 Description = "30 minutes of exercise per day",
                 UserID = userId
             }
        );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique(); // Ensure email is unique
        });
    }
}
