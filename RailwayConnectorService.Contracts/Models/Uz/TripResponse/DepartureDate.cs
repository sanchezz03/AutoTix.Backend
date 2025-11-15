using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.TripResponse;

public class DepartureDate
{
    [JsonProperty("dates")]
    public List<string> Dates { get; set; }
}
