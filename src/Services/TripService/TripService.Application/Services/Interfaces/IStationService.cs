using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;

namespace TripService.Application.Services.Interfaces;

public interface IStationService
{
    Task<List<Station>> GetStationsAsync();
    Task<List<Station>> GetStationBoardsAsync();
    Task<StationBoard> GetStationBoardAsync(long id);
}
