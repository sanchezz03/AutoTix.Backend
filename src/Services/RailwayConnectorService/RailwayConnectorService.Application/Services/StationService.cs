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

    public async Task<List<Station>> GetStationsAsync(string accessToken)
    {
        return await _webService.GetStationsAsync(accessToken);
    }

    public async Task<List<Station>> GetStationBoardsAsync(string accessToken)
    {
        return await _webService.GetStationBoardsAsync(accessToken);
    }

    public async Task<StationBoard> GetStationBoardAsync(long id, string accessToken)
    {
        return await _webService.GetStationBoardAsync(id, accessToken);
    }
}
