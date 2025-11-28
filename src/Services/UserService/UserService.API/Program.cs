using UserService.Application.Extensions;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("UserService");

builder.Services
    .AddHttpContextAccessor()
    .AddControllers().Services
    .AddDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAuthServices(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddDataProtection().Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.ApplyMigrations();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();