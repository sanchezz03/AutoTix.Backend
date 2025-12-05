using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class WagonByClass
{
    [JsonProperty("wagons")]
    public List<Wagon> Wagons { get; set; }

    [JsonProperty("monitoring")]
    public WagonMonitoring Monitoring { get; set; }

    [JsonProperty("train_direction")]
    public object TrainDirection { get; set; }
}
