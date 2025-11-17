using Microsoft.Extensions.DependencyInjection;
using RailwayConnectorService.Application.Services;
using RailwayConnectorService.Application.Services.Interfaces;

namespace RailwayConnectorService.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStationService, StationService>()
            .AddScoped<ITripService, TripService>()
            .AddScoped<IAuthService, AuthService>();
    }
}
