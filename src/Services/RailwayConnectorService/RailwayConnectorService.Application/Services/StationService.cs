using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Services;

public class StationService : IStationService
{
    private readonly IStationWebService _webService;
    private readonly ICacheService _cacheService;

    public StationService(IStationWebService webService, ICacheService cacheService)
    {
        _webService = webService;
        _cacheService = cacheService;
    }

    public async Task<List<Station>> GetStationsAsync(string uzAccessToken)
    {
        var cacheKey = "station:all";
        var cached = await _cacheService.GetAsync<List<Station>>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _webService.GetStationsAsync(uzAccessToken);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(1));
        return response;
    }

    public async Task<List<Station>> GetStationBoardsAsync(string uzAccessToken)
    {
        var cacheKey = "station:boards";
        var cached = await _cacheService.GetAsync<List<Station>>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _webService.GetStationBoardsAsync(uzAccessToken);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(1));
        return response;
    }

    public async Task<StationBoard> GetStationBoardAsync(long id, string uzAccessToken)
    {
        var cacheKey = $"station:board:{id}";
        var cached = await _cacheService.GetAsync<StationBoard>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _webService.GetStationBoardAsync(id, uzAccessToken);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(1));
        return response;
    }
}
