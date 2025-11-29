using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;

namespace RailwayConnectorService.Infrastructure.External.Services;

public abstract class BaseWebService
{
    protected readonly HttpClient _httpClient;
    protected readonly ILogger _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string X_SESSION_ID = "db1eedc6-2e2d-4c68-af8f-2e6b844b6e98";

    public BaseWebService(string httpClientName, IHttpClientFactory httpClientFactory, ILogger logger, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    protected async Task<T> GetAsync<T>(string url, string? accessToken = null)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            ApplyAuthorizationHeader(request, accessToken);
            ApplyUzHeaders(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error($"HTTP GET failed: {response.StatusCode}, Content: {content}");
                throw new HttpRequestException($"HTTP {response.StatusCode}: {content}");
            }

            return JsonConvert.DeserializeObject<T>(content)
                   ?? throw new InvalidOperationException("Response deserialization returned null");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, nameof(GetAsync));
            throw;
        }
    }

    protected async Task<T> PostAsync<T>(string url, object? payload = null, string? accessToken = null)
    {
        try
        {
            HttpContent? content = payload != null
                ? new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json")
                : new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            ApplyAuthorizationHeader(request, accessToken);      
            ApplyUzHeaders(request);

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error($"HTTP POST failed: {response.StatusCode}, Content: {responseContent}");
                throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(responseContent)
                   ?? throw new InvalidOperationException("Response deserialization returned null");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, nameof(PostAsync));
            throw;
        }
    }


    #region Private methods

    private void ApplyAuthorizationHeader(HttpRequestMessage request, string? accessToken = null)
    {
        request.Headers.Remove("Authorization");

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return;
        }

        #region TEST BY BEARER TOKEN
        
        //var authHeader = _httpContextAccessor.HttpContext?
        //    .Request.Headers["Authorization"]
        //    .ToString();

        //if (!string.IsNullOrEmpty(authHeader))
        //{
        //    request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        //}

        #endregion
    }

    private void ApplyUzHeaders(HttpRequestMessage request)
    {
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        request.Headers.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en-US"));
        request.Headers.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en", 0.9));
        request.Headers.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("ru-RU", 0.8));
        request.Headers.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("ru", 0.7));
        request.Headers.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("uk", 0.6));

        request.Headers.Add("Origin", "https://booking.uz.gov.ua");
        request.Headers.Add("Referer", "https://booking.uz.gov.ua/");
        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/142.0.0.0 Safari/537.36");
        request.Headers.Add("x-client-locale", "uk");
        request.Headers.Add("x-user-agent", "UZ/2 Web/1 User/3499184");

        request.Headers.Add("x-session-id", X_SESSION_ID);
    }

    #endregion
}
