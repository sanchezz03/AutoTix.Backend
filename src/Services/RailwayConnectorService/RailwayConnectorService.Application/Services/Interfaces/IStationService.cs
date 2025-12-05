using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IStationService
{
    Task<List<Station>> GetStationsAsync(string uzAccessToken);
    Task<List<Station>> GetStationBoardsAsync(string uzAccessToken);
    Task<StationBoard> GetStationBoardAsync(long id, string uzAccessToken);
}
