using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OrderService.Infrastructure.Extensions;

public static class LoggerExtension
{
    public static IHostBuilder AddLogger(this IHostBuilder hostBuilder, string serviceName, bool isWriteConsole = true)
    {
        return hostBuilder.UseSerilog((hostingContext, services, _inSeriesLoggerConfiguration) =>
        {
            _inSeriesLoggerConfiguration
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Marten", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Internal", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Cors.Infrastructure", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Server.Kestrel", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Extensions.Http", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning);

            if (isWriteConsole)
            {
                _inSeriesLoggerConfiguration.WriteTo.Console();
            }
        });
    }
}

