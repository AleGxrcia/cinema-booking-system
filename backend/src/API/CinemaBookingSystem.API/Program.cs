using CinemaBookingSystem.Api.Extensions;
using CinemaBookingSystem.Api.Middlewares;
using CinemaBookingSystem.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSharedInfrastructure(builder.Configuration);

var moduleAssemblies = new[]
{
    typeof(CinemaBookingSystem.Modules.Movies.AssemblyReference).Assembly,
};

builder.Services.AddModules(builder.Configuration, moduleAssemblies);

var app = builder.Build();

app.UseExceptionHandler();

app.UseModules();

app.Run();
