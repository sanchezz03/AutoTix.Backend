
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface IStationWebService
{
    Task<List<Station>> GetStationsAsync();
    Task<List<Station>> GetStationBoardsAsync();
    Task<StationBoard> GetStationBoardAsync(long id);
}
