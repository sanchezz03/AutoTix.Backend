using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using TripService.Application.Interfaces;
using TripService.Infrastructure.Configuration;
using TripService.Infrastructure.External.Models;
using TripService.Infrastructure.External.UserService.Services;

namespace TripService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UserServiceApiOptions>(configuration.GetSection("UserServiceApi"));
        
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

        services.AddScoped<IUserServiceClient, UserServiceClient>();

        return services;
    }
}
