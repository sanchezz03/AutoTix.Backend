using Microsoft.Extensions.DependencyInjection;
using TripService.Application.Services;
using TripService.Application.Services.Interfaces;

namespace TripService.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStationService, StationService>();
    }
}