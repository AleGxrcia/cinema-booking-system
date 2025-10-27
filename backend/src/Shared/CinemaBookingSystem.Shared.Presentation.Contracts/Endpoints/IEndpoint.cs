using Microsoft.AspNetCore.Routing;

namespace CinemaBookingSystem.Shared.Presentation.Contracts.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
