using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.StationResponse;

namespace RailwayConnectorService.Application.Services;

public class StationService : IStationService
{
    private readonly IStationWebService _webService;

    public StationService(IStationWebService webService)
    {
        _webService = webService;
    }

    public async Task<UzResponse<List<Station>>> GetStationsAsync()
    {
        return await _webService.GetStationsAsync();
    }

    public async Task<UzResponse<List<Station>>> GetStationBoardsAsync()
    {
        return await _webService.GetStationBoardsAsync();
    }

    public async Task<UzResponse<StationBoard>> GetStationBoardAsync(long id)
    {
        return await _webService.GetStationBoardAsync(id);
    }
}
