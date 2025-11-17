using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

namespace RailwayConnectorService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthWebService _authWebService;

    public AuthService(IAuthWebService authWebService)
    {
        _authWebService = authWebService;
    }

    public Task<UzResponse<SendSms>> SendSmsAsync(SendSmsRequest request)
    {
        return _authWebService.SendSmsAsync(request);
    }

    public Task<UzResponse<Login>> LoginAsync(LoginRequest request)
    {
        return _authWebService.LoginAsync(request);
    }

    public Task LogoutAsync()
    {
        return _authWebService.LogoutAsync();
    }
}
