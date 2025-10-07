using System.Diagnostics;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingSystem.Api.Middlewares;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        logger.LogError(exception,
            "Unhandled exception occurred. TraceId: {TraceId}, Path: {Path}, Method: {Method}",
            traceId,
            httpContext.Request.Path,
            httpContext.Request.Method
        );

        var (error, statusCode) = MapExceptionToError(exception, traceId);

        var problemDetails = CreateProblemDetails(
            error,
            statusCode,
            traceId,
            httpContext,
            exception
        );

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    
    private (Error Error, int StatusCode) MapExceptionToError(
        Exception ex,
        string traceId)
    {
        var isDevelopment = environment.IsDevelopment();

        return ex switch
        {
            DomainException domainEx => (
                Error.Failure("DOMAIN_ERROR", domainEx.Message),
                StatusCodes.Status400BadRequest
            ),

            // Validation Errors (from ValidationBehavior)
            _ when ex.Message.Contains("VALIDATION_ERROR") => (
                Error.Validation("VALIDATION_ERROR", ex.Message),
                StatusCodes.Status400BadRequest
            ),

            ArgumentNullException argNull => (
                Error.Validation("NULL_ARGUMENT",
                    $"Parameter '{argNull.ParamName}' is required."),
                StatusCodes.Status400BadRequest
            ),

            ArgumentException argEx => (
                Error.Validation("INVALID_ARGUMENT",
                    isDevelopment ? argEx.Message : "Invalid request parameter."),
                StatusCodes.Status400BadRequest
            ),

            UnauthorizedAccessException => (
                Error.Unauthorized("UNAUTHORIZED", "Authentication required."),
                StatusCodes.Status401Unauthorized
            ),

            InvalidOperationException invalidOp => (
                Error.Conflict("INVALID_OPERATION",
                    isDevelopment ? invalidOp.Message
                                : "The requested operation is not valid in the current state."),
                StatusCodes.Status409Conflict
            ),

            TimeoutException => (
                Error.Unexpected("TIMEOUT", "Operation timed out. Please try again."),
                StatusCodes.Status504GatewayTimeout
            ),

            TaskCanceledException => (
                Error.Failure("REQUEST_CANCELLED", "The operation was cancelled."),
                StatusCodes.Status408RequestTimeout
            ),

            HttpRequestException httpEx => (
                Error.Unexpected("EXTERNAL_SERVICE_ERROR",
                    isDevelopment ? httpEx.Message
                                : $"External service error. TraceId: {traceId}"),
                StatusCodes.Status502BadGateway
            ),

            _ => (
                Error.Unexpected("UNHANDLED_ERROR",
                    isDevelopment ? ex.Message
                                : $"An unexpected error occurred. TraceId: {traceId}"),
                StatusCodes.Status500InternalServerError
            )
        };
    }

    private ProblemDetails CreateProblemDetails(
        Error error,
        int statusCode,
        string traceId,
        HttpContext httpContext,
        Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Type = GetProblemType(error.Type),
            Title = GetTitle(error.Type),
            Status = statusCode,
            Detail = error.Message,
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["traceId"] = traceId,
                ["errorCode"] = error.Code,
                ["errorType"] = error.Type.ToString(),
                ["timestamp"] = DateTime.UtcNow,
            }
        };

        if (httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
        {
            problemDetails.Extensions["correlationId"] = correlationId.ToString();
        }

        if (environment.IsDevelopment())
        {
            problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
        }

        return problemDetails;
    }

    private static string GetProblemType(ErrorType errorType) => errorType switch
    {
        ErrorType.Validation or ErrorType.Failure => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
        ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
        ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
        ErrorType.Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
        ErrorType.Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
        _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
    };

    private static string GetTitle(ErrorType errorType) => errorType switch
    {
        ErrorType.Validation => "Validation Error",
        ErrorType.NotFound => "Resource Not Found",
        ErrorType.Conflict => "Conflict",
        ErrorType.Failure => "Bad Request",
        ErrorType.Unauthorized => "Unauthorized",
        ErrorType.Forbidden => "Forbidden",
        ErrorType.Unexpected => "Internal Server Error",
        _ => "An error occurred"
    };
}
