using RailwayConnectorService.API.Grpc;
using RailwayConnectorService.Application.Extensions;
using RailwayConnectorService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("RailwayConnectorService");

builder.Services
    .AddHttpContextAccessor()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddGrpc().Services
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGrpcService<AuthGrpcService>();
app.MapGrpcService<StationGrpcService>();
app.MapGrpcService<TripGrpcService>();

app.Run();