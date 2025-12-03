namespace TripService.Application.Services.Interfaces;

public interface IStationService
{
    Task<List<object>> GetTripsAsync();
}
