using RailwayConnectorService.Application.Extensions;
using RailwayConnectorService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("RailwayConnectorService");

builder.Services
    .AddHttpContextAccessor()
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();