using CinemaBookingSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var moduleAssemblies = new[]
{
    typeof(CinemaBookingSystem.Modules.Movies.AssemblyReference).Assembly,
};

builder.Services.AddModules(builder.Configuration, moduleAssemblies);

var app = builder.Build();

app.UseModules();

app.Run();
