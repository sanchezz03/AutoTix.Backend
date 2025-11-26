using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class DepartureDate
{
    [JsonProperty("dates")]
    public List<string> Dates { get; set; }
}
