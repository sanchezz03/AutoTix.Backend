using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;

public class StationBoard
{
    [JsonProperty("station")]
    public Station Station { get; set; }

    [JsonProperty("arrivals")]
    public List<Arrival> Arrivals { get; set; }

    [JsonProperty("departures")]
    public List<Departure> Departures { get; set; }

    [JsonProperty("peron")]
    public bool Peron { get; set; }
}
