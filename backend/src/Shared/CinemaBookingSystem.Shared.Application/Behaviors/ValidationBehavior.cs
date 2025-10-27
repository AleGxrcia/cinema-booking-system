using CinemaBookingSystem.Shared.Domain.Common;
using FluentValidation;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            var validationErrors = failures
                .Select(f => new ValidationErrorDetail(
                    PropertyName: f.PropertyName,
                    ErrorCode: f.ErrorCode,
                    ErrorMessage: f.ErrorMessage
                ))
                .ToList();

            var error = Error.ValidationWithDetails(
                "VALIDATION_ERROR",
                "One or more validation errors occurred.",
                validationErrors
            );

            return (TResponse)(object)Result.Failure(error);
        }

        return await next(cancellationToken);
    }
}
