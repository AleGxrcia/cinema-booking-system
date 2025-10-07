using CinemaBookingSystem.Api.Extensions;
using CinemaBookingSystem.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var moduleAssemblies = new[]
{
    typeof(CinemaBookingSystem.Modules.Movies.AssemblyReference).Assembly,
};

builder.Services.AddModules(builder.Configuration, moduleAssemblies);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseModules();

app.Run();
