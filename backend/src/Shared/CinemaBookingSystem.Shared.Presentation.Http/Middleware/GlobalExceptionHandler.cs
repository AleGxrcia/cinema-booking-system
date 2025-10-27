using System.Diagnostics;
using CinemaBookingSystem.Shared.Domain.Common;
using CinemaBookingSystem.Shared.Presentation.Contracts.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Shared.Presentation.Http.Middleware;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment environment,
    IHttpResponseFactory httpResponseFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        logger.LogError(exception,
            "Unhandled exception occurred. TraceId: {TraceId}, Path: {Path}, Method: {Method}",
            traceId,
            httpContext.Request.Path,
            httpContext.Request.Method
        );

        var error = MapExceptionToError(exception, traceId);
        var problemDetails = httpResponseFactory.CreateProblemDetails(error, httpContext);

        if (environment.IsDevelopment())
        {
            problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
        }

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private Error MapExceptionToError(Exception ex, string traceId)
    {
        var isDevelopment = environment.IsDevelopment();

        return ex switch
        {
            DomainException domainEx => Error.Failure("DOMAIN_ERROR", domainEx.Message),

            ArgumentNullException argNull => Error.Validation(
                "NULL_ARGUMENT",
                $"Parameter '{argNull.ParamName}' is required."),

            ArgumentException argEx => Error.Validation(
                "INVALID_ARGUMENT",
                isDevelopment ? argEx.Message : "Invalid request parameter."),

            UnauthorizedAccessException => Error.Unauthorized(
                "UNAUTHORIZED",
                "Authentication required."),

            InvalidOperationException invalidOp => Error.Conflict(
                "INVALID_OPERATION",
                isDevelopment ? invalidOp.Message
                            : "The requested operation is not valid in the current state."),

            TimeoutException => Error.Unexpected(
                "TIMEOUT",
                "Operation timed out. Please try again."),

            TaskCanceledException => Error.Failure(
                "REQUEST_CANCELLED",
                "The operation was cancelled."),

            HttpRequestException httpEx => Error.Unexpected(
                "EXTERNAL_SERVICE_ERROR",
                isDevelopment ? httpEx.Message
                            : $"External service error. TraceId: {traceId}"),

            _ => Error.Unexpected(
                "UNHANDLED_ERROR",
                isDevelopment ? ex.Message
                            : $"An unexpected error occurred. TraceId: {traceId}")
        };
    }
}
