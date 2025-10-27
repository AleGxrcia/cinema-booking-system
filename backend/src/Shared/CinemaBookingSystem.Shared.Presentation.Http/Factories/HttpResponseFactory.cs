using CinemaBookingSystem.Shared.Domain.Common;
using CinemaBookingSystem.Shared.Presentation.Contracts.Abstractions;
using CinemaBookingSystem.Shared.Presentation.Contracts.Models;
using CinemaBookingSystem.Shared.Presentation.Http.Extensions;
using CinemaBookingSystem.Shared.Presentation.Http.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CinemaBookingSystem.Shared.Presentation.Http.Factories;

public class HttpResponseFactory : IHttpResponseFactory
{
    private readonly IHostEnvironment _environment;

    public HttpResponseFactory(IHostEnvironment environment)
    {
        _environment = environment;
    }

    public ProblemDetails CreateProblemDetails(Error error, HttpContext httpContext)
    {
        var statusCode = ErrorMappers.GetStatusCode(error.Type);
        var traceId = httpContext.GetTraceId();
        var correlationId = httpContext.GetCorrelationId();

        var problemDetails = new ProblemDetails
        {
            Type = ErrorMappers.GetProblemType(error.Type),
            Title = ErrorMappers.GetTitle(error.Type),
            Detail = error.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["errorCode"] = error.Code,
                ["errorType"] = error.Type.ToString(),
                ["timestamp"] = DateTime.UtcNow,
                ["traceId"] = traceId
            }
        };

        if (correlationId is not null)
        {
            problemDetails.Extensions["correlationId"] = correlationId;
        }

        if (error.ValidationErrors != null && error.ValidationErrors.Count > 0)
        {
            var validationErrors = error.ValidationErrors
                .Select(ve => new ValidationError(
                    Field: ve.PropertyName,
                    Code: ve.ErrorCode,
                    Message: ve.ErrorMessage
                ))
                .ToList();

            problemDetails.Extensions["errors"] = validationErrors;
        }

        if (_environment.IsDevelopment())
        {
            problemDetails.Extensions["environment"] = "Development";
        }

        return problemDetails;
    }
}
