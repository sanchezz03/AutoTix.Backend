using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TripService.Application.DTOs.Response.UserService;
using TripService.Application.Interfaces;
using TripService.Infrastructure.External.Models;

namespace TripService.Infrastructure.External.UserService.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _client;
    private readonly ILogger _logger;

    public UserServiceClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _client = httpClientFactory.CreateClient(HttpClientName.USER_SERVICE);
        _logger = logger;
    }

    public async Task<UzAccessTokenResult> GetRailwayTokenAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/auth/uz-access-token");
        ApplyAuthorizationHeader(request);

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var uzAccessToken = await response.Content.ReadFromJsonAsync<UzAccessTokenResult>();
        return uzAccessToken!;
    }

    #region Private methods
    private void ApplyAuthorizationHeader(HttpRequestMessage request)
    {
        var authHeader = _httpContextAccessor.HttpContext?
            .Request.Headers["Authorization"]
            .ToString();

        if (!string.IsNullOrEmpty(authHeader))
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }
    }

    #endregion
}
