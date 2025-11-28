using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Configuration;
using UserService.Infrastructure.Security;

namespace UserService.Infrastructure.Extensions;

public static class AuthServiceExtension
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        return services
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IEncryptionService, DataProtectionEncryptionService>();
    }
}
