using FluentValidation;
using Habits.Application.Database;
using Habits.Application.Repositories;
using Habits.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Habits.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Registar DbContext
        services.AddDbContext<HabitTrackerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Outros serviços
        services.AddScoped<IHabitRepository, HabitRepository>();
        services.AddScoped<IHabitService, HabitService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Scoped);

        return services;
    }
}