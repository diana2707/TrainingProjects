using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Persistance;
using AirportTool.Infrastructure.Repositories;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTool.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AirportDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AirportDb")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPendingEntitiesService, PendingEntitiesService>();

        services.AddScoped<IFlightsRepository, FlightsRepository>();
        services.AddScoped<IAircraftRepository, AircraftsRepository>();
        services.AddScoped<IAirportRepository, AirportsRepository>();
        services.AddScoped<IAirlineRepository, AirlinesRepository>();
        services.AddScoped<IGateRepository, GatesRepository>();
        services.AddScoped<ISchedulesRepository, SchedulesRepository>();
        services.AddScoped<ITicketsRepository, TicketsRepository>();
        services.AddScoped<IBookingRepository, BookingsRepository>();

        services.AddScoped<PendingEntitiesService>();

        return services;
    }
}

