using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Repositories;
using UserService.Infrastructure.Configuration;
using UserService.Infrastructure.External.RailwayConnector.Services;
using UserService.Infrastructure.Protos;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RailwayConnectorGrpcOptions>(
            configuration.GetSection("GrpcClients:RailwayConnector")
        );

        services.AddGrpcClient<AuthServiceGrpc.AuthServiceGrpcClient>((sp, o) =>
        {
            var opts = sp.GetRequiredService<IOptions<RailwayConnectorGrpcOptions>>().Value;
            o.Address = new Uri(opts.Url);
        });


        return services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUzCredentialRepository, UzCredentialRepository>()
            .AddScoped<IRailwayConnetorClient, RailwayConnectorClient>();
    }
}
