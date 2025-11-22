using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<SendSms> SendSmsAsync(SendSmsRequest request);
    Task<Login> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
