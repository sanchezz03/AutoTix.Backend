using OrderService.API.Extensions;
using OrderService.Application.Commands.Handlers;
using OrderService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("OrderService");

builder.Services
    .AddHttpContextAccessor()
    .AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(AddReservationHandler).Assembly)
    )
    .AddControllers().Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddRabbitMq()
    .AddEndpointsApiExplorer()
    .AddCORS();

var app = builder.Build();

app.ApplyMigrations();

app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();