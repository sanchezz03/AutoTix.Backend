using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface ITripWebService
{
    Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, string uzAccessToken, bool withTransfers = false);
    Task<Direct> GetTripAsync(long tripId, string uzAccessToken);
    Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId, string uzAccessToken);
    Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass, string uzAccessToken);
}
