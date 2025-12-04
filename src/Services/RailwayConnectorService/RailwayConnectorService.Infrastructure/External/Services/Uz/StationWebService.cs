using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using Serilog;

namespace RailwayConnectorService.Infrastructure.External.Services.Uz;

public class StationWebService : BaseWebService, IStationWebService
{
    private readonly string _baseUrl;

    public StationWebService(IHttpClientFactory httpClientFactory, ILogger logger, IOptions<UzApiOptions> options, IHttpContextAccessor httpContextAccessor)
         : base(HttpClientName.UZ, httpClientFactory, logger, httpContextAccessor)
    {
        _baseUrl = options.Value.BaseUrl;
    }

    public Task<List<Station>> GetStationsAsync(string accessToken)
    {
        var url = $"{_baseUrl}stations?search=";
        return GetAsync<List<Station>>(url, accessToken);
    }

    public Task<List<Station>> GetStationBoardsAsync(string accessToken)
    {
        var url = $"{_baseUrl}station-boards";
        return GetAsync<List<Station>>(url, accessToken);
    }

    public Task<StationBoard> GetStationBoardAsync(long id, string accessToken)
    {
        var url = $"{_baseUrl}station-boards/{id}";
        return GetAsync<StationBoard>(url, accessToken);
    }
}
