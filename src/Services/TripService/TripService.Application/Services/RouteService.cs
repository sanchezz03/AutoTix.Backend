using TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;
using TripService.Application.Interfaces;
using TripService.Application.Services.Interfaces;

namespace TripService.Application.Services;

public class RouteService : IRouteService
{
    private readonly IRailwayConnectorService _railwayConnectorService;
    private readonly IUserServiceClient _userServiceClient;

    public RouteService(IRailwayConnectorService railwayConnectorService, IUserServiceClient userServiceClient)
    {
        _railwayConnectorService = railwayConnectorService;
        _userServiceClient = userServiceClient;
    }

    public async Task<Direct> GetTripAsync(long tripId)
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetTripAsync(tripId, uzAccessToken.AccessToken);
    }

    public async Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, bool withTransfers = false)
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetTripAsync(stationFromId, stationToId, date, withTransfers, uzAccessToken.AccessToken);
    }

    public async Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId)
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetDepartureDatesAsync(stationFromId, stationToId, uzAccessToken.AccessToken);
    }

    public async Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass)
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetWagonsByClassAsync(tripId, wagonClass, uzAccessToken.AccessToken);
    }
}
