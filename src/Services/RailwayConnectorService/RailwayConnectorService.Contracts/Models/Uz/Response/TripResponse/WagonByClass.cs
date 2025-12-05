using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class WagonByClass
{
    [JsonProperty("wagons")]
    public List<Wagon> Wagons { get; set; }

    [JsonProperty("monitoring")]
    public WagonMonitoring Monitoring { get; set; }

    [JsonProperty("train_direction")]
    public object TrainDirection { get; set; }
}
