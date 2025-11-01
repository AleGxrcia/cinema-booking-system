using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Shared.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private const int SlowRequestThresholdSeconds = 3;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var responseType = typeof(TResponse).Name;

        _logger.LogInformation(
            "Processing request {RequestName} expecting {ResponseType}",
            requestName,
            responseType);

        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var response = await next(cancellationToken);
            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;

            if (elapsedMs > SlowRequestThresholdSeconds)
            {
                _logger.LogWarning(
                    "Slow request detected: {RequestName} took {ElapsedMilliseconds}ms ({ElapsedSeconds:F2}s) to complete. Request: {@Request}",
                    requestName,
                    elapsedMs,
                    stopwatch.Elapsed.TotalSeconds,
                    request);
            }
            else
            {
                _logger.LogInformation(
                    "Request {RequestName} completed successfully in {ElapsedMilliseconds}ms",
                    requestName,
                    stopwatch.Elapsed.Milliseconds);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex,
                "Request {RequestName} failed after {ElapsedMilliseconds}ms. Request: {@Request}",
                requestName,
                stopwatch.Elapsed.Milliseconds,
                request);
            throw;
        }
    }
}
