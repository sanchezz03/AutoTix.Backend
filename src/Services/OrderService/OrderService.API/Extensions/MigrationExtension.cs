using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Persistence;

namespace OrderService.API.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetService<OrderDbContext>()
                .Database.Migrate();
        }
    }
}

