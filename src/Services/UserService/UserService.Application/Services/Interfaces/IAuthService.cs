using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

namespace UserService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<SendSms> SendSmsAsync(SendSmsRequest request);
    Task<Login> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
