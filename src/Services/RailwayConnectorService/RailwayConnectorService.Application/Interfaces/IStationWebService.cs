
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface IStationWebService
{
    Task<UzResponse<List<Station>>> GetStationsAsync();
    Task<UzResponse<List<Station>>> GetStationBoardsAsync();
    Task<UzResponse<StationBoard>> GetStationBoardAsync(long id);
}
