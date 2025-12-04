using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;
using TripService.Application.Interfaces;
using TripService.Application.Services.Interfaces;

namespace TripService.Application.Services;

public class StationService : IStationService
{
    private readonly IRailwayConnectorService _railwayConnectorService;
    private readonly IUserServiceClient _userServiceClient;

    public StationService(IRailwayConnectorService railwayConnectorService, IUserServiceClient userServiceClient)
    {
        _railwayConnectorService = railwayConnectorService;
        _userServiceClient = userServiceClient;
    }

    public async Task<List<Station>> GetStationsAsync()
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetStationsAsync(uzAccessToken.AccessToken);
    }

    public async Task<List<Station>> GetStationBoardsAsync()
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetStationBoardsAsync(uzAccessToken.AccessToken);
    }

    public async Task<StationBoard> GetStationBoardAsync(long id)
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();
        return await _railwayConnectorService.GetStationBoardAsync(id, uzAccessToken.AccessToken);
    }
}
