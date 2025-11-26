using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;
using UserService.Application.Interfaces;
using UserService.Application.Services.Interfaces;

namespace UserService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRailwayConnetorClient _railwayConnetorClient;

    public AuthService(IRailwayConnetorClient railwayConnetorClient)
    {
        _railwayConnetorClient = railwayConnetorClient;
    }

    public async Task<SendSms> SendSmsAsync(SendSmsRequest request)
    {
        return await _railwayConnetorClient.SendSmsAsync(request);
    }
    public async Task<Login> LoginAsync(LoginRequest request)
    {
        return await _railwayConnetorClient.LoginAsync(request);
    }

    public async Task LogoutAsync()
    {
        await _railwayConnetorClient.LogoutAsync();
    }
}
