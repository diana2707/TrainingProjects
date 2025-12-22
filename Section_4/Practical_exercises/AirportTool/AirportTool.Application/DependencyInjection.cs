using AirportTool.Application.Contracts;
using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Mappers;
using AirportTool.Application.Services;
using AirportTool.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTool.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IFlightsService, FlightsService>();
        services.AddScoped<ISchedulesService, SchedulesService>();
        services.AddScoped<ITicketsService, TicketsService>();
        services.AddScoped<IBookingsService, BookingService>();

        // Mappers
        services.AddScoped<IFlightMapper, FlightMapper>();
        services.AddScoped<ISchedulesMapper, SchedulesMapper>();
        services.AddScoped<ITicketMapper, TicketMapper>();
        services.AddScoped<IBookingMapper, BookingMapper>();

        // Validators
        services.AddScoped<ISchedulesValidator, SchedulesValidator>();
        services.AddScoped<IBookingValidator, BookingValidator>();

        return services;
    }
}

