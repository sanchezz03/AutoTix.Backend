using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Interfaces;

public interface ITripWebService
{
    Task<UzResponse<List<Direct>>> GetTripsAsync(int stationFromId, int stationToId, string date, bool withTransfers = false);
    Task<UzResponse<Direct>> GetTripAsync(int tripId);
    Task<UzResponse<List<string>>> GetDepartureDatesAsync(int stationFromId, int stationToId);
}
