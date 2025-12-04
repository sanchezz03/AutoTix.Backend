using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using TripService.Application.Interfaces;
using TripService.Infrastructure.Configuration;
using TripService.Infrastructure.External.Models;
using TripService.Infrastructure.External.RailwayConnector.Services;
using TripService.Infrastructure.External.UserService.Services;
using TripService.Infrastructure.Protos;

namespace TripService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UserServiceApiOptions>(configuration.GetSection("UserServiceApi"));
        services.Configure<RailwayConnectorGrpcOptions>(
            configuration.GetSection("GrpcClients:RailwayConnector")
        );

        services.AddGrpcClient<StationServiceGrpc.StationServiceGrpcClient>((sp, o) =>
        {
            var opts = sp.GetRequiredService<IOptions<RailwayConnectorGrpcOptions>>().Value;
            o.Address = new Uri(opts.Url);
        });

        var cookieContainer = new CookieContainer();

        services.AddHttpClient(HttpClientName.USER_SERVICE, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<UserServiceApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = cookieContainer,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });

        return services
            .AddScoped<IUserServiceClient, UserServiceClient>()
            .AddScoped<IRailwayConnectorService, RailwayConnectorClient>();
    }
}
