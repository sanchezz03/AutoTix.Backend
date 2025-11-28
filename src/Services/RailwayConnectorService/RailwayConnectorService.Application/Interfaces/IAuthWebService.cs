using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;
namespace RailwayConnectorService.Application.Interfaces;

public interface IAuthWebService
{
    Task<SendSms> SendSmsAsync(SendSmsRequest request);
    Task<Login> LoginAsync(LoginRequest request);
    Task LogoutAsync(LogoutRequest request);
}
