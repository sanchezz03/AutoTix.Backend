using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;

namespace TripService.Application.Interfaces;

public interface IRailwayConnectorService
{
    Task<List<Station>> GetStationsAsync(string accessToken);
    Task<List<Station>> GetStationBoardsAsync(string accessToken);
    Task<StationBoard> GetStationBoardAsync(long stationId, string accessToken);
}
