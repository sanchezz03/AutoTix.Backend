using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface IStationService
{
    Task<List<Station>> GetStationsAsync();
    Task<List<Station>> GetStationBoardsAsync();
    Task<StationBoard> GetStationBoardAsync(long id);
}
