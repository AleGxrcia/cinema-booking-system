using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Domain.Common;
using CinemaBookingSystem.Shared.Presentation.Contracts.Abstractions;
using CinemaBookingSystem.Shared.Presentation.Contracts.Models;
using Microsoft.AspNetCore.Http;

namespace CinemaBookingSystem.Shared.Presentation.Http.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(
        this Result<T> result,
        HttpContext httpContext,
        IHttpResponseFactory httpResponseFactory)
    {
        if (result.IsSuccess)
        {
            return CreateSuccessResponse(result.Value, httpContext);
        }

        return CreateErrorResponse(result.Error, httpContext, httpResponseFactory);
    }

    public static IResult ToHttpResult(
        this Result result,
        HttpContext httpContext,
        IHttpResponseFactory httpResponseFactory)
    {
        if (result.IsSuccess)
        {
            return CreateSuccessResponse<object?>(null, httpContext);
        }

        return CreateErrorResponse(result.Error, httpContext, httpResponseFactory);
    }

    public static IResult ToPagedHttpResult<T>(
        this PagedResult<T> result,
        HttpContext httpContext,
        IHttpResponseFactory httpResponseFactory)
    {
        if (result.IsSuccess)
        {
            return CreatePagedResponse(result, httpContext);
        }

        return CreateErrorResponse(result.Error, httpContext, httpResponseFactory);
    }

    private static IResult CreateSuccessResponse<T>(T data, HttpContext httpContext)
    {
        var metadata = httpContext.CreateResponseMetadata();
        var response = ApiResponse<T>.Succeed(data, metadata);
        return Results.Ok(response);
    }

    private static IResult CreatePagedResponse<T>(PagedResult<T> pagedResult, HttpContext httpContext)
    {
        var metadata = httpContext.CreateResponseMetadata();
        
        var response = PagedResponse<T>.Succeed(
            pagedResult.Value,
            pagedResult.PageNumber,
            pagedResult.PageSize,
            pagedResult.TotalCount,
            pagedResult.TotalPages,
            pagedResult.HasNextPage,
            pagedResult.HasPreviousPage,
            metadata
        );

        return Results.Ok(response);
    }

    private static IResult CreateErrorResponse(Error error, HttpContext httpContext, IHttpResponseFactory httpResponseFactory)
    {
        var problemDetails = httpResponseFactory.CreateProblemDetails(error, httpContext);
        return Results.Problem(problemDetails);
    }
}