using CinemaBookingSystem.Api.Extensions;
using CinemaBookingSystem.Shared.Infrastructure;
using CinemaBookingSystem.Shared.Presentation.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedPresentationHttp();
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
