
using AirportTool.Application.Paging;
using AirportTool.WebApi.Middleware;
using AirportTool.WebApi.Settings;
using AirportTool.Application;
using AirportTool.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configuration
builder.Services.Configure<ImportSettings>(
    builder.Configuration.GetSection("ImportSettings"));

builder.Services.Configure<PagingOptions>(
    builder.Configuration.GetSection("PagingOptions"));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Logging
builder.Logging.AddConsole();

var app = builder.Build();

// Middleware
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
