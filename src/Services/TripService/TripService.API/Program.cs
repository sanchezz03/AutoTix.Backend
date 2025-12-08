using TripService.Application.Extensions;
using TripService.Infrastructure.Extensions;
using TripService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("TripService");

builder.Services
    .AddHttpContextAccessor()
    .AddControllers().Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddCORS();

var app = builder.Build();

app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();