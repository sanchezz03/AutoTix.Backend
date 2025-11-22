using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface ITripWebService
{
    Task<List<Direct>> GetTripsAsync(int stationFromId, int stationToId, string date, bool withTransfers = false);
    Task<Direct> GetTripAsync(int tripId);
    Task<List<string>> GetDepartureDatesAsync(int stationFromId, int stationToId);
}
