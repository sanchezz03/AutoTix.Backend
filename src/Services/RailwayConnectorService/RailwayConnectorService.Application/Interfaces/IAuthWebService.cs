using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;
namespace RailwayConnectorService.Application.Interfaces;

public interface IAuthWebService
{
    Task<UzResponse<SendSms>> SendSmsAsync(SendSmsRequest request);
    Task<UzResponse<Login>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
