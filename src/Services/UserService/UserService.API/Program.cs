using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger("UserService");

builder.Services
    .AddHttpContextAccessor()
    .AddControllers().Services
    .AddDatabase(builder.Configuration)
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.ApplyMigrations();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();