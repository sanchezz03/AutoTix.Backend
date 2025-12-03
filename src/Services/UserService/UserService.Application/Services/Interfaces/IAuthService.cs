using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

namespace UserService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<SendSms> SendSmsAsync(SendSmsRequest request);
    Task<AuthResult> LoginAsync(LoginRequest request);
    Task<UzAccessTokenResult> GetUzAccessTokenAsync(long userId);
    Task LogoutAsync(long userId);
}
