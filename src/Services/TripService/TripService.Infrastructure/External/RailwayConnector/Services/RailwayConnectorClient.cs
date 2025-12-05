using TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;
using TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;
using TripService.Application.Interfaces;
using TripService.Infrastructure.Protos.Station;
using TripService.Infrastructure.Protos.Trip;

namespace TripService.Infrastructure.External.RailwayConnector.Services;

public class RailwayConnectorClient : IRailwayConnectorService
{
    private readonly StationServiceGrpc.StationServiceGrpcClient _stationGrpc;
    private readonly TripServiceGrpc.TripServiceGrpcClient _tripGrpc;

    public RailwayConnectorClient(StationServiceGrpc.StationServiceGrpcClient stationGrpc,
        TripServiceGrpc.TripServiceGrpcClient tripGrpc)
    {
        _stationGrpc = stationGrpc;
        _tripGrpc = tripGrpc;
    }

    #region StationGrpc methods

    public async Task<List<Station>> GetStationsAsync(string accessToken)
    {
        var response = await _stationGrpc.GetStationsAsync(new Empty(), headers: BuildHeaders(accessToken));

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
        var response = await _stationGrpc.GetStationBoardsAsync(new Empty(), headers: BuildHeaders(accessToken));

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
        var response = await _stationGrpc.GetStationBoardAsync(
            new StationBoardRequest { Id = stationId },
            headers: BuildHeaders(accessToken));

        return MapBoard(response.Board);
    }

    #endregion

    #region TripGrpc methods

    public async Task<Direct> GetTripAsync(long tripId, string accessToken = "")
    {
        var request = new TripByIdRequest { TripId = tripId };
        var response = await _tripGrpc.GetTripByIdAsync(request, headers: BuildHeaders(accessToken));

        return MapDirect(response.Trip);
    }

    public async Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, bool withTransfers = false, string accessToken = "")
    {
        var request = new TripRequest
        {
            StationFromId = stationFromId,
            StationToId = stationToId,
            Date = date,
            WithTransfers = withTransfers
        };

        var response = await _tripGrpc.GetTripAsync(request, headers: BuildHeaders(accessToken));
        return MapTrip(response.Trip);
    }

    public async Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId, string accessToken = "")
    {
        var request = new DepartureDatesRequest
        {
            StationFromId = stationFromId,
            StationToId = stationToId
        };

        var response = await _tripGrpc.GetDepartureDatesAsync(request, headers: BuildHeaders(accessToken)); 

        return response.Dates.ToList();
    }

    public async Task<WagonByClass> GetWagonsByClassAsync(long tripId, string wagonClass, string accessToken = "")
    {
        var request = new WagonByClassRequest
        {
            TripId = tripId,
            WagonClass = wagonClass
        };

        var response = await _tripGrpc.GetWagonsByClassAsync(request, headers: BuildHeaders(accessToken));

        return MapWagonByClass(response.Data);
    }

    #endregion

    #region Private methods

    private Trip MapTrip(GrpcTrip grpcTrip)
    {
        return new Trip
        {
            StationFrom = grpcTrip.StationFrom,
            StationTo = grpcTrip.StationTo,
            WithTransfer = grpcTrip.WithTransfer,
            Monitoring = grpcTrip.Monitoring,
            Direct = grpcTrip.Direct.Select(MapTripSegment).ToList()
        };
    }

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

    private TripSegment MapTripSegment(GrpcTripSegment grpcTripSegment)
    {
        return new TripSegment
        {
            Id = grpcTripSegment.Id,
            DepartAt = grpcTripSegment.DepartAt,
            ArriveAt = grpcTripSegment.ArriveAt,
            StationFrom = grpcTripSegment.StationFrom ?? "",
            StationTo = grpcTripSegment.StationTo ?? "",
            StationsTimeOffset = grpcTripSegment.StationsTimeOffset,
            Train = grpcTripSegment.Train != null ? MapTrain(grpcTripSegment.Train) : null,
            Discount = grpcTripSegment.Discount,
            CustomTag = grpcTripSegment.CustomTag,
            Monitoring = grpcTripSegment.Monitoring != null
                ? new Monitoring
                {
                    Allowed = grpcTripSegment.Monitoring.Allowed,
                    AutoPurchase = grpcTripSegment.Monitoring.AutoPurchase
                }
                : null,
            IsDeparted = grpcTripSegment.IsDeparted
        };
    }

    private Direct MapDirect(GrpcDirect grpcDirect)
    {
        return new Direct
        {
            Id = grpcDirect.Id,
            DepartAt = grpcDirect.DepartAt,
            ArriveAt = grpcDirect.ArriveAt,
            StationFrom = grpcDirect.StationFrom != null
            ? new Station
            {
                Id = grpcDirect.StationFrom.Id,
                Name = grpcDirect.StationFrom.Name ?? ""
            }
            : new Station { Id = 0, Name = "" },
            StationTo = grpcDirect.StationTo != null
            ? new Station
            {
                Id = grpcDirect.StationTo.Id,
                Name = grpcDirect.StationTo.Name ?? ""
            }
            : new Station { Id = 0, Name = "" },
            StationsTimeOffset = grpcDirect.StationsTimeOffset,
            Train = grpcDirect.Train != null ? MapTrain(grpcDirect.Train) : null,
            Discount = grpcDirect.Discount,
            CustomTag = grpcDirect.CustomTag,
            Monitoring = grpcDirect.Monitoring != null
                ? new Monitoring
                {
                    Allowed = grpcDirect.Monitoring.Allowed,
                    AutoPurchase = grpcDirect.Monitoring.AutoPurchase
                }
                : null,
            IsDeparted = grpcDirect.IsDeparted
        };
    }

    private Train MapTrain(GrpcTrain grpcTrain)
    {
        return new Train
        {
            Id = grpcTrain.Id,
            StationFrom = grpcTrain.StationFrom,
            StationTo = grpcTrain.StationTo,
            Number = grpcTrain.Number,
            Type = grpcTrain.Type,
            InfoPopup = grpcTrain.InfoPopup,
            WagonClasses = grpcTrain.WagonClasses.Select(MapWagonClass).ToList()
        };
    }

    private WagonClass MapWagonClass(GrpcWagonClass grpcWagon)
    {
        return new WagonClass
        {
            Id = grpcWagon.Id,
            Name = grpcWagon.Name,
            FreeSeats = grpcWagon.FreeSeats,
            Price = grpcWagon.Price,
            Amenities = grpcWagon.Amenities.ToList()
        };
    }

    private WagonByClass MapWagonByClass(GrpcWagonByClass grpc)
    {
        return new WagonByClass
        {
            Wagons = grpc.Wagons.Select(MapWagon).ToList(),
            Monitoring = grpc.Monitoring != null
                ? new WagonMonitoring
                {
                    AvailableType = grpc.Monitoring.AvailableType,
                    Active = grpc.Monitoring.Active
                }
                : null,
            TrainDirection = grpc.TrainDirection
        };
    }

    private Wagon MapWagon(GrpcWagon g)
    {
        return new Wagon
        {
            Id = g.Id,
            Number = g.Number,
            MockupName = g.MockupName,
            Seats = g.Seats.ToList(),
            FreeSeatsTop = g.FreeSeatsTop,
            FreeSeatsLower = g.FreeSeatsLower,
            Price = g.Price,
            AirConditioner = g.AirConditioner,
            Services = g.Services.Select(MapService).ToList(),
            Privileges = g.Privileges.Select(MapPrivilege).ToList()
        };
    }

    private Service MapService(GrpcService s)
    {
        return new Service
        {
            Id = s.Id,
            Title = s.Title,
            Price = s.Price,
            SelectType = s.SelectType,
            SelectUnitsMax = s.SelectUnitsMax,
            SelectedByDefault = s.SelectedByDefault,
            Details = s.Details != null
                ? new Details
                {
                    Photo = s.Details.Photo,
                    Content = s.Details.Content
                        .Select(c => new Content { Title = c.Title, Description = c.Description })
                        .ToList()
                }
                : null
        };
    }

    private Privilege MapPrivilege(GrpcPrivilege p)
    {
        return new Privilege
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            InputType = p.InputType,
            Active = p.Active,
            CompanionId = p.CompanionId,
            Rules = p.Rules,
            Hint = p.Hint != null
                ? new Hint
                {
                    Name = p.Hint.Name,
                    Image = p.Hint.Image,
                    Text = p.Hint.Text,
                    ButtonText = p.Hint.ButtonText
                }
                : null
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