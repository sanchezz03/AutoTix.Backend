using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RailwayConnectorService.API.Protos.Station;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

namespace RailwayConnectorService.API.Grpc;

public class StationGrpcService : StationServiceGrpc.StationServiceGrpcBase
{
    private readonly IStationService _stationService;

    public StationGrpcService(IStationService stationService)
    {
        _stationService = stationService;
    }

    public override async Task<StationsResponse> GetStations(Empty request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var stations = await _stationService.GetStationsAsync(uzAccessToken);

        return new StationsResponse
        {
            Stations = { stations.Select(s => new GrpcStation { Id = s.Id, Name = s.Name }) }
        };
    }

    public override async Task<StationBoardsResponse> GetStationBoards(Empty request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var stationBoards = await _stationService.GetStationBoardsAsync(uzAccessToken);

        return new StationBoardsResponse
        {
            Stations = { stationBoards.Select(s => new GrpcStation { Id = s.Id, Name = s.Name }) }
        };
    }

    public override async Task<StationBoardResponse> GetStationBoard(StationBoardRequest request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var board = await _stationService.GetStationBoardAsync(request.Id, uzAccessToken);

        return new StationBoardResponse
        {
            Board = MapBoard(board)
        };
    }

    #region Private methods

    private string ExtractUzAccessToken(ServerCallContext context)
    {
        var raw = context.RequestHeaders.GetValue("Authorization");
        if (raw == null) return "";

        return raw.Replace("Bearer ", "");
    }

    private GrpcStationBoard MapBoard(StationBoard board)
    {
        return new GrpcStationBoard
        {
            Station = board.Station != null
                    ? new GrpcStation
                    {
                        Id = (int)board.Station.Id,
                        Name = board.Station.Name ?? ""
                    }
                    : new GrpcStation { Id = 0, Name = "" },

            Arrivals =
        {
            (board.Arrivals ?? Enumerable.Empty<Arrival>()).Select(a => new GrpcArrival
            {
                Train = a.Train ?? "",
                Route = a.Route ?? "",
                Time = a.Time,
                Platform = a.Platform ?? "",
                DelayMinutes = a.DelayMinutes ?? 0
            })
        },

            Departures =
        {
            (board.Departures ?? Enumerable.Empty<Departure>()).Select(d => new GrpcDeparture
            {
                Train = d.Train ?? "",
                Route = d.Route ?? "",
                Time = d.Time,
                Platform = d.Platform ?? ""
            })
        },

            Peron = board.Peron
        };
    }


    #endregion
}
