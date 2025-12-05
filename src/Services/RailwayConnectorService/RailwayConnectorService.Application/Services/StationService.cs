using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Services;

public class StationService : IStationService
{
    private readonly IStationWebService _webService;

    public StationService(IStationWebService webService)
    {
        _webService = webService;
    }

    public async Task<List<Station>> GetStationsAsync(string uzAccessToken)
    {
        return await _webService.GetStationsAsync(uzAccessToken);
    }

    public async Task<List<Station>> GetStationBoardsAsync(string uzAccessToken)
    {
        return await _webService.GetStationBoardsAsync(uzAccessToken);
    }

    public async Task<StationBoard> GetStationBoardAsync(long id, string uzAccessToken)
    {
        return await _webService.GetStationBoardAsync(id, uzAccessToken);
    }
}
