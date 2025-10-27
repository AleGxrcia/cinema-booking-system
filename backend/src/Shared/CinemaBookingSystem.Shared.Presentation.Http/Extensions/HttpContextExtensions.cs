using System.Diagnostics;
using CinemaBookingSystem.Shared.Presentation.Contracts.Models;
using Microsoft.AspNetCore.Http;

namespace CinemaBookingSystem.Shared.Presentation.Http.Extensions;

public static class HttpContextExtensions
{
    public static string GetTraceId(this HttpContext httpContext)
    {
        return Activity.Current?.Id ?? httpContext.TraceIdentifier;
    }

    public static string? GetCorrelationId(this HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
        {
            return correlationId.ToString();
        }

        return null;
    }

    public static ResponseMetadata CreateResponseMetadata(this HttpContext httpContext)
    {
        return new ResponseMetadata(
            Timestamp: DateTime.UtcNow,
            TraceId: httpContext.GetTraceId(),
            CorrelationId: httpContext.GetCorrelationId()
        );
    }   
}
