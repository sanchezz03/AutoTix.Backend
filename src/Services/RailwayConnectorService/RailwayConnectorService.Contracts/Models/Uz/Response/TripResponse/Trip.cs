using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Trip
{
    [JsonProperty("station_from")]
    public string StationFrom { get; set; }

    [JsonProperty("station_to")]
    public string StationTo { get; set; }

    [JsonProperty("direct")]
    public List<Direct> Direct { get; set; }

    [JsonProperty("with_transfer")]
    public object WithTransfer { get; set; }

    [JsonProperty("monitoring")]
    public object Monitoring { get; set; }
}
