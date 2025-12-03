using TripService.Application.DTOs.Response.UserService;

namespace TripService.Application.Interfaces;

public interface IUserServiceClient
{
    Task<UzAccessTokenResult> GetRailwayTokenAsync();
}
