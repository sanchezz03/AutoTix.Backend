using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.TripResponse;

public class Trip
{
    [JsonProperty("station_from")]
    public string StationFrom;

    [JsonProperty("station_to")]
    public string StationTo;

    [JsonProperty("direct")]
    public List<Direct> Direct;

    [JsonProperty("with_transfer")]
    public object WithTransfer;

    [JsonProperty("monitoring")]
    public object Monitoring;
}
