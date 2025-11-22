using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Services;
using UserService.Application.Services.Interfaces;

namespace UserService.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthService, AuthService>();
    }
}
