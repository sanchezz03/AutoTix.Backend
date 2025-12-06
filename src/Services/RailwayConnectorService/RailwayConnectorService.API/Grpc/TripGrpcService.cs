using Grpc.Core;
using RailwayConnectorService.API.Protos.Trip;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.API.Grpc;

public class TripGrpcService : TripServiceGrpc.TripServiceGrpcBase
{
    private readonly ITripService _tripService;

    public TripGrpcService(ITripService tripService)
    {
        _tripService = tripService;
    }

    public override async Task<TripResponse> GetTrip(TripRequest request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var trip = await _tripService.GetTripAsync(request.StationFromId, request.StationToId, request.Date, uzAccessToken, request.WithTransfers);

        var grpcTrip = new GrpcTrip
        {
            StationFrom = trip.StationFrom ?? "",
            StationTo = trip.StationTo ?? "",
            WithTransfer = trip.WithTransfer?.ToString() ?? "",
            Monitoring = trip.Monitoring?.ToString() ?? ""
        };

        if (trip.Direct != null)
            grpcTrip.Direct.AddRange(trip.Direct.Select(MapTripSegment));

        return new TripResponse { Trip = grpcTrip };
    }

    public override async Task<RouteResponse> GetTripById(TripByIdRequest request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var direct = await _tripService.GetTripAsync(request.TripId, uzAccessToken);

        return new RouteResponse
        {
            Trip = MapDirect(direct)
        };
    }

    public override async Task<DepartureDatesResponse> GetDepartureDates(DepartureDatesRequest request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);
        var date = await _tripService.GetDepartureDatesAsync(request.StationFromId, request.StationToId, uzAccessToken);

        var response = new DepartureDatesResponse
        {
            Dates = new GrpcDepartureDates()
        };
        if (date?.Dates != null)
        {
            response.Dates.Dates.AddRange(date.Dates);
        }

        return response;
    }

    public override async Task<WagonByClassResponse> GetWagonsByClass(WagonByClassRequest request, ServerCallContext context)
    {
        var uzAccessToken = ExtractUzAccessToken(context);

        var wagonByClass = await _tripService.GetWagonByClassAsync(request.TripId, request.WagonClass, uzAccessToken);

        return new WagonByClassResponse
        {
            Data = MapWagonByClass(wagonByClass)
        };
    }

    #region Private methods

    private string ExtractUzAccessToken(ServerCallContext context)
    {
        var raw = context.RequestHeaders.GetValue("Authorization");
        if (raw == null) return "";

        return raw.Replace("Bearer ", "");
    }

    private GrpcTripSegment MapTripSegment(TripSegment direct)
    {
        return new GrpcTripSegment
        {
            Id = direct.Id,
            DepartAt = direct.DepartAt,
            ArriveAt = direct.ArriveAt,
            StationFrom = direct.StationFrom ?? "",
            StationTo = direct.StationTo ?? "",
            StationsTimeOffset = direct.StationsTimeOffset,
            Train = direct.Train != null ? MapTrain(direct.Train) : null,
            Discount = direct.Discount?.ToString() ?? "",
            CustomTag = direct.CustomTag?.ToString() ?? "",
            Monitoring = direct.Monitoring != null ? new GrpcMonitoring
            {
                Allowed = direct.Monitoring.Allowed,
                AutoPurchase = direct.Monitoring.AutoPurchase
            } : null,
            IsDeparted = direct.IsDeparted
        };
    }

    private GrpcDirect MapDirect(Direct direct)
    {
        return new GrpcDirect
        {
            Id = direct.Id,
            DepartAt = direct.DepartAt,
            ArriveAt = direct.ArriveAt,
            StationFrom = direct.StationFrom != null
                ? new GrpcStation { Id = direct.StationFrom.Id, Name = direct.StationFrom.Name ?? "" }
                : new GrpcStation { Id = 0, Name = "" },
            StationTo = direct.StationTo != null
                ? new GrpcStation { Id = direct.StationTo.Id, Name = direct.StationTo.Name ?? "" }
                : new GrpcStation { Id = 0, Name = "" },
            StationsTimeOffset = direct.StationsTimeOffset,
            Train = direct.Train != null ? MapTrain(direct.Train) : null,
            Discount = direct.Discount?.ToString() ?? "",
            CustomTag = direct.CustomTag?.ToString() ?? "",
            Monitoring = direct.Monitoring != null ? new GrpcMonitoring
            {
                Allowed = direct.Monitoring.Allowed,
                AutoPurchase = direct.Monitoring.AutoPurchase
            } : null,
            IsDeparted = direct.IsDeparted
        };
    }

    private GrpcTrain MapTrain(Train train)
    {
        var grpcTrain = new GrpcTrain
        {
            Id = train.Id,
            StationFrom = train.StationFrom ?? "",
            StationTo = train.StationTo ?? "",
            Number = train.Number ?? "",
            Type = train.Type,
            InfoPopup = train.InfoPopup?.ToString() ?? ""
        };

        grpcTrain.WagonClasses.AddRange(train.WagonClasses?.Select(MapWagonClass) ?? Enumerable.Empty<GrpcWagonClass>());

        return grpcTrain;
    }

    private GrpcWagonClass MapWagonClass(WagonClass wagon)
    {
        return new GrpcWagonClass
        {
            Id = wagon.Id ?? "",
            Name = wagon.Name ?? "",
            FreeSeats = wagon.FreeSeats,
            Price = wagon.Price,
            Amenities = { wagon.Amenities ?? new List<string>() }
        };
    }

    private GrpcWagonByClass MapWagonByClass(WagonByClass src)
    {
        var grpc = new GrpcWagonByClass();

        if (src.Wagons != null)
            grpc.Wagons.AddRange(src.Wagons.Select(MapWagon));

        grpc.Monitoring = src.Monitoring != null
            ? new GrpcMonitoringWagon
            {
                AvailableType = src.Monitoring.AvailableType ?? "",
                Active = src.Monitoring.Active?.ToString() ?? ""
            }
            : null;

        grpc.TrainDirection = src.TrainDirection?.ToString() ?? "";

        return grpc;
    }

    private GrpcWagon MapWagon(Wagon w)
    {
        var grpc = new GrpcWagon
        {
            Id = w.Id ?? "",
            Number = w.Number ?? "",
            MockupName = w.MockupName ?? "",
            FreeSeatsTop = w.FreeSeatsTop,
            FreeSeatsLower = w.FreeSeatsLower,
            Price = w.Price,
            AirConditioner = w.AirConditioner
        };

        grpc.Seats.AddRange(w.Seats ?? new List<int>());

        grpc.Services.AddRange(w.Services?.Select(MapService) ?? Enumerable.Empty<GrpcService>());
        grpc.Privileges.AddRange(w.Privileges?.Select(MapPrivilege) ?? Enumerable.Empty<GrpcPrivilege>());

        return grpc;
    }

    private GrpcService MapService(Service s)
    {
        return new GrpcService
        {
            Id = s.Id ?? "",
            Title = s.Title ?? "",
            Details = MapDetails(s.Details),
            Price = s.Price,
            SelectType = s.SelectType ?? "",
            SelectUnitsMax = s.SelectUnitsMax?.ToString() ?? "",
            SelectedByDefault = s.SelectedByDefault
        };
    }

    private GrpcDetails MapDetails(Details d)
    {
        if (d == null) return null;

        var grpc = new GrpcDetails
        {
            Photo = d.Photo ?? ""
        };

        grpc.Content.AddRange(d.Content?.Select(c => new GrpcContent
        {
            Title = c.Title ?? "",
            Description = c.Description ?? ""
        }) ?? Enumerable.Empty<GrpcContent>());

        return grpc;
    }

    private GrpcPrivilege MapPrivilege(Privilege p)
    {
        return new GrpcPrivilege
        {
            Id = p.Id,
            Name = p.Name ?? "",
            Description = p.Description ?? "",
            InputType = p.InputType,
            Active = p.Active,
            CompanionId = p.CompanionId ?? 0,
            Rules = p.Rules ?? "",
            Hint = MapHint(p.Hint)
        };
    }

    private GrpcHint MapHint(Hint h)
    {
        if (h == null) return null;

        return new GrpcHint
        {
            Name = h.Name ?? "",
            Image = h.Image ?? "",
            Text = h.Text ?? "",
            ButtonText = h.ButtonText ?? ""
        };
    }

    #endregion
}
