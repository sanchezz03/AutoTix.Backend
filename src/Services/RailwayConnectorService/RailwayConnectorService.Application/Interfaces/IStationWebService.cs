
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface IStationWebService
{
    Task<List<Station>> GetStationsAsync(string accessToken);
    Task<List<Station>> GetStationBoardsAsync(string accessToken);
    Task<StationBoard> GetStationBoardAsync(long id, string accessToken);
}
