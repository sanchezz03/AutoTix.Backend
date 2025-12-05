
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface IStationWebService
{
    Task<List<Station>> GetStationsAsync(string uzAccessToken);
    Task<List<Station>> GetStationBoardsAsync(string uzAccessToken);
    Task<StationBoard> GetStationBoardAsync(long id, string uzAccessToken);
}
