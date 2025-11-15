using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using RailwayConnectorService.Infrastructure.External.Services.Uz;

namespace RailwayConnectorService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UzApiOptions>(configuration.GetSection("UzApi"));

        services
            .AddScoped<IStationWebService, StationWebService>()
            .AddScoped<ITripWebService, TripWebService>()
            .AddHttpClient(HttpClientName.UZ, (sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<UzApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }
}
