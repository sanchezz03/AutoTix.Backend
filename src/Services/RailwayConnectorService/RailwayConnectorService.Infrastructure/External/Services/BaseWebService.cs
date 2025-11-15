using Newtonsoft.Json;
using RailwayConnectorService.Contracts.Models.Uz;
using Serilog;

namespace RailwayConnectorService.Infrastructure.External.Services;

public abstract class BaseWebService
{
    protected readonly HttpClient _httpClient;
    protected readonly ILogger _logger;

    public BaseWebService(string httpClientName, IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
        _logger = logger;
    }

    protected async Task<UzResponse<T>> GetAsync<T>(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(content);
                return new UzResponse<T>(data);
            }
            else
            {
                _logger.Error($"HTTP request failed with status code: {response.StatusCode}");
                return new UzResponse<T>(default);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, nameof(GetAsync));
            return new UzResponse<T>(default);
        }
    }
}
