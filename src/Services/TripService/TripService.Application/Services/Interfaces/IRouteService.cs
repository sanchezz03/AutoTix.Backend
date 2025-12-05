using TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

namespace TripService.Application.Services.Interfaces;

public interface IRouteService
{
    Task<Direct> GetTripAsync(long tripId);
    Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, bool withTransfers = false);
    Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId);
    Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass);
}
