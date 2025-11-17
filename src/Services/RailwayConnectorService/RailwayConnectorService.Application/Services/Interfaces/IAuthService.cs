using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<UzResponse<SendSms>> SendSmsAsync(SendSmsRequest request);
    Task<UzResponse<Login>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
