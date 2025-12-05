using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;
using TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

namespace TripService.Application.Interfaces;

public interface IRailwayConnectorService
{
    Task<List<Station>> GetStationsAsync(string accessToken);
    Task<List<Station>> GetStationBoardsAsync(string accessToken);
    Task<StationBoard> GetStationBoardAsync(long stationId, string accessToken);

    Task<Direct> GetTripAsync(long tripId, string accessToken = "");
    Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, bool withTransfers = false, string accessToken = "");
    Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId, string accessToken = "");
    Task<WagonByClass> GetWagonsByClassAsync(long tripId, string wagonClass, string accessToken = "");
}
