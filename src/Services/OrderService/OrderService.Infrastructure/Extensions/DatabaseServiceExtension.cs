using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Extensions;

public static class DatabaseServiceExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        return services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
