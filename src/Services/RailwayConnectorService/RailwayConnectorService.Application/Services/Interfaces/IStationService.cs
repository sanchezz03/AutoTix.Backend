using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IStationService
{
    Task<List<Station>> GetStationsAsync(string accessToken);
    Task<List<Station>> GetStationBoardsAsync(string accessToken);
    Task<StationBoard> GetStationBoardAsync(long id, string accessToken);
}
