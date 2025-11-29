using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Direct
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("depart_at")]
    public int DepartAt { get; set; }

    [JsonProperty("arrive_at")]
    public int ArriveAt { get; set; }

    [JsonProperty("station_from")]
    public string StationFrom { get; set; }

    [JsonProperty("station_to")]
    public string StationTo { get; set; }

    [JsonProperty("stations_time_offset")]
    public int StationsTimeOffset { get; set; }

    [JsonProperty("train")]
    public Train Train { get; set; }

    [JsonProperty("discount")]
    public object Discount { get; set; }

    [JsonProperty("custom_tag")]
    public object CustomTag { get; set; }

    [JsonProperty("monitoring")]
    public Monitoring Monitoring { get; set; }

    [JsonProperty("is_departed")]
    public bool IsDeparted { get; set; }
}
