using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;
using TripService.Application.Interfaces;
using TripService.Infrastructure.Protos;

namespace TripService.Infrastructure.External.RailwayConnector.Services;

public class RailwayConnectorClient : IRailwayConnectorService
{
    private readonly StationServiceGrpc.StationServiceGrpcClient _grpc;

    public RailwayConnectorClient(StationServiceGrpc.StationServiceGrpcClient grpc)
    {
        _grpc = grpc;
    }

    public async Task<List<Station>> GetStationsAsync(string accessToken)
    {
        var response = await _grpc.GetStationsAsync(new Empty(), headers: BuildHeaders(accessToken));

        return response.Stations
            .Select(s => new Station
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToList();
    }

    public async Task<List<Station>> GetStationBoardsAsync(string accessToken)
    {
        var response = await _grpc.GetStationBoardsAsync(new Empty(), headers: BuildHeaders(accessToken));

        return response.Boards
            .Select(s => new Station
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToList();
    }

    public async Task<StationBoard> GetStationBoardAsync(long stationId, string accessToken)
    {
        var response = await _grpc.GetStationBoardAsync(
            new StationBoardRequest { Id = stationId },
            headers: BuildHeaders(accessToken));

        return MapBoard(response.Board);
    }

    #region Private methods

    private StationBoard MapBoard(GrpcStationBoard board)
    {
        return new StationBoard
        {
            Station = new Station
            {
                Id = board.Station.Id,
                Name = board.Station.Name
            },
            Peron = board.Peron,

            Arrivals = board.Arrivals.Select(a => new Arrival
            {
                Train = a.Train,
                Route = a.Route,
                Time = a.Time,
                Platform = a.Platform,
                DelayMinutes = a.DelayMinutes
            }).ToList(),

            Departures = board.Departures.Select(d => new Departure
            {
                Train = d.Train,
                Route = d.Route,
                Time = d.Time,
                Platform = d.Platform
            }).ToList()
        };
    }

    private Grpc.Core.Metadata BuildHeaders(string token)
    {
        return new Grpc.Core.Metadata
        {
            { "Authorization", $"Bearer {token}" }
        };
    }

    #endregion
}