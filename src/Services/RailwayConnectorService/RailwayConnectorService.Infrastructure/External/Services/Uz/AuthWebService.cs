using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;
using RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using Serilog;

namespace RailwayConnectorService.Infrastructure.External.Services.Uz;

public class AuthWebService : BaseWebService, IAuthWebService
{
    private readonly string _baseUrl;

    public AuthWebService(IHttpClientFactory httpClientFactory, ILogger logger, IOptions<UzApiOptions> options)
         : base(HttpClientName.UZ, httpClientFactory, logger)
    {
        _baseUrl = options.Value.BaseUrl;
    }

    public async Task<SendSms> SendSmsAsync(SendSmsRequest request)
    {
        var payload = new { phone = request.Phone };
        var url = $"{_baseUrl}auth/send-sms";
        return await PostAsync<SendSms>(url, payload);
    }

    public async Task<Login> LoginAsync(LoginRequest request)
    {
        var payload = new
        {
            phone = request.Phone,
            code = request.Code,
            device = new
            {
                name = request.Device.Name,
                fcm_token = (string?)null
            }
        };

        var url = $"{_baseUrl}v2/auth/login";
        return await PostAsync<Login>(url, payload);
    }

    public async Task LogoutAsync(LogoutRequest request)
    {
        var url = $"{_baseUrl}auth/logout";
        await PostAsync<object?>(url, null, request.AccessToken);
    }
}
