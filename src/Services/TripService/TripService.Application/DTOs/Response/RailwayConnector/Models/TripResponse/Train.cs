using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class Train
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("station_from")]
    public string StationFrom { get; set; }

    [JsonProperty("station_to")]
    public string StationTo { get; set; }

    [JsonProperty("number")]
    public string Number { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("wagon_classes")]
    public List<WagonClass> WagonClasses { get; set; }

    [JsonProperty("info_popup")]
    public object InfoPopup { get; set; }

    [JsonProperty("info")]
    public object Info { get; set; }
}
