using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class DepartureDate
{
    [JsonProperty("dates")]
    public List<string> Dates { get; set; }
}
