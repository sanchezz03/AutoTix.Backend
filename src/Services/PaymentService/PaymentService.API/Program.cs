using PaymentService.API.Extensions;
using PaymentService.Application.Extensions;
using PaymentService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("OrderService");

builder.Services
    .AddHttpContextAccessor()
    .AddControllers().Services
    .AddBackgroundJobs()
    .AddApplicationServices()
    .AddRabbitMq()
    .AddEndpointsApiExplorer()
    .AddCORS();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();