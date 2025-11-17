using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using Serilog;

namespace RailwayConnectorService.Infrastructure.External.Services.Uz;

public class StationWebService : BaseWebService, IStationWebService
{
    private readonly string _baseUrl;

    public StationWebService(IHttpClientFactory httpClientFactory, ILogger logger,
        IHttpContextAccessor httpContextAccessor, IOptions<UzApiOptions> options)
         : base(HttpClientName.UZ, httpClientFactory, logger, httpContextAccessor)
    {
        _baseUrl = options.Value.BaseUrl;
    }

    public Task<UzResponse<List<Station>>> GetStationsAsync()
    {
        var url = $"{_baseUrl}stations?search=";
        return GetAsync<List<Station>>(url);
    }

    public Task<UzResponse<List<Station>>> GetStationBoardsAsync()
    {
        var url = $"{_baseUrl}station-boards";
        return GetAsync<List<Station>>(url);
    }

    public Task<UzResponse<StationBoard>> GetStationBoardAsync(long id)
    {
        var url = $"{_baseUrl}station-boards/{id}";
        return GetAsync<StationBoard>(url);
    }
}
