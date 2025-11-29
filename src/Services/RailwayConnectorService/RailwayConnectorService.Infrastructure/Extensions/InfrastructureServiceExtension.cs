using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using RailwayConnectorService.Infrastructure.External.Services.Uz;
using RailwayConnectorService.Infrastructure.Services;
using StackExchange.Redis;
using System.Net;

namespace RailwayConnectorService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UzApiOptions>(configuration.GetSection("UzApi"));
        services.Configure<RedisOptions>(configuration.GetSection("Redis"));

        var cookieContainer = new CookieContainer();

        services.AddHttpClient(HttpClientName.UZ, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<UzApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
       .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
       {
           UseCookies = true,
           CookieContainer = cookieContainer,
           AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
       });

        services
            //Singleton Services
            .AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
                return ConnectionMultiplexer.Connect(options.ConnectionString);
            })

            //Scoped Services
            .AddScoped<IStationWebService, StationWebService>()
            .AddScoped<ITripWebService, TripWebService>()
            .AddScoped<IAuthWebService, AuthWebService>()
            .AddScoped<ICacheService, CacheService>();

        return services;
    }
}
