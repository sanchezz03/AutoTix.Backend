using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

namespace RailwayConnectorService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthWebService _authWebService;
    private readonly ICacheService _cacheService;

    public AuthService(IAuthWebService authWebService, ICacheService cacheService)
    {
        _authWebService = authWebService;
        _cacheService = cacheService;
    }

    public async Task<SendSms> SendSmsAsync(SendSmsRequest request)
    {
        var cacheKey = $"auth:sms:{request.Phone}";

        var cached = await _cacheService.GetAsync<SendSms>(cacheKey);
        //if (cached != null)
        //    return cached;

        var response = await _authWebService.SendSmsAsync(request);

        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(1));

        return response;
    }

    public async Task<Login> LoginAsync(LoginRequest request)
    {
        var cacheKey = $"auth:login:{request.Phone}:{request.Code}";

        //var cached = await _cacheService.GetAsync<Login>(cacheKey);
        //if (cached != null)
        //    return cached;

        var response = await _authWebService.LoginAsync(request);

        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }

    public async Task LogoutAsync(LogoutRequest request)
    {
        await _authWebService.LogoutAsync(request);
    }
}
