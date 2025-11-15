using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.StationResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IStationService
{
    Task<UzResponse<List<Station>>> GetStationsAsync();
    Task<UzResponse<List<Station>>> GetStationBoardsAsync();
    Task<UzResponse<StationBoard>> GetStationBoardAsync(long id);
}
