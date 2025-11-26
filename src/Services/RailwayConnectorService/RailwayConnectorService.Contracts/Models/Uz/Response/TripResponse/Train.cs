using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Train
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("station_from")]
    public string StationFrom;

    [JsonProperty("station_to")]
    public string StationTo;

    [JsonProperty("number")]
    public string Number;

    [JsonProperty("type")]
    public int Type;

    [JsonProperty("wagon_classes")]
    public List<WagonClass> WagonClasses;

    [JsonProperty("info_popup")]
    public object InfoPopup;
}
