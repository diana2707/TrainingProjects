using AirportTool.Application.Contracts;
using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Mappers;
using AirportTool.Application.Services;
using AirportTool.Application.Validators;
using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Persistance;
using AirportTool.Infrastructure.Repositories;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using AirportTool.WebApi.Middleware;
using AirportTool.WebApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Database => try to move db connection to separate config file
builder.Services.AddDbContext<AirportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirportDb")));

// Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<PendingEntitiesService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<IAircraftRepository, AircraftsRepository>();
builder.Services.AddScoped<IAirportRepository, AirportsRepository>();
builder.Services.AddScoped<IAirlineRepository, AirlinesRepository>();
builder.Services.AddScoped<IGateRepository, GatesRepository>();
builder.Services.AddScoped<ISchedulesRepository, SchedulesRepository>();
builder.Services.AddScoped<ITicketsRepository, TicketsRepository>();
builder.Services.AddScoped<IBookingRepository, BookingsRepository>();

builder.Services.AddScoped<IFlightMapper, FlightMapper>();
builder.Services.AddScoped<ISchedulesMapper, SchedulesMapper>();
builder.Services.AddScoped<ITicketMapper, TicketMapper>();
builder.Services.AddScoped<IBookingMapper, BookingMapper>();

builder.Services.AddScoped<IFlightsService, FlightsService>();
builder.Services.AddScoped<ISchedulesService, SchedulesService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();
builder.Services.AddScoped<IBookingsService, BookingService>();

builder.Services.AddScoped<ISchedulesValidator, SchedulesValidator>();
builder.Services.AddScoped<IBookingValidator, BookingValidator>();

// Configurations
builder.Services.Configure<ImportSettings>(
    builder.Configuration.GetSection("ImportSettings"));

builder.Services.Configure<PagingOptions>(
    builder.Configuration.GetSection("PagingOptions"));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Logging
builder.Logging.AddConsole();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
