using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Services.Interfaces;

public interface ITripService
{
    Task<List<Direct>> GetTripAsync(int stationFromId, int stationToId, string date, bool withTransfers = false);
    Task<Direct> GetTripAsync(int tripId);
    Task<List<string>> GetDepartureDatesAsync(int stationFromId, int stationToId);
}
