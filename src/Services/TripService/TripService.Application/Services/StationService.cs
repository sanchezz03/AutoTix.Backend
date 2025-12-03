using TripService.Application.Interfaces;
using TripService.Application.Services.Interfaces;

namespace TripService.Application.Services;

public class StationService : IStationService
{
    private readonly IUserServiceClient _userServiceClient;

    public StationService(IUserServiceClient userServiceClient)
    {
        _userServiceClient = userServiceClient;
    }

    public async Task<List<object>> GetTripsAsync()
    {
        var uzAccessToken = await _userServiceClient.GetRailwayTokenAsync();

        return new List<object>();
    }
}
