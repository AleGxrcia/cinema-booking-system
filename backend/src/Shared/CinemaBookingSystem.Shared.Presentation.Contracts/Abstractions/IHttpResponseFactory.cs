using CinemaBookingSystem.Shared.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingSystem.Shared.Presentation.Contracts.Abstractions;

public interface IHttpResponseFactory
{
    ProblemDetails CreateProblemDetails(Error error, HttpContext httpContext);
}
