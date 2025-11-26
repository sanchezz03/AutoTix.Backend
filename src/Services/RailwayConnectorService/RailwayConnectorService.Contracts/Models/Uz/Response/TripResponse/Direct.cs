using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Direct
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("depart_at")]
    public int DepartAt;

    [JsonProperty("arrive_at")]
    public int ArriveAt;

    [JsonProperty("station_from")]
    public string StationFrom;

    [JsonProperty("station_to")]
    public string StationTo;

    [JsonProperty("stations_time_offset")]
    public int StationsTimeOffset;

    [JsonProperty("train")]
    public Train Train;

    [JsonProperty("discount")]
    public object Discount;

    [JsonProperty("custom_tag")]
    public object CustomTag;

    [JsonProperty("monitoring")]
    public Monitoring Monitoring;

    [JsonProperty("is_departed")]
    public bool IsDeparted;
}
