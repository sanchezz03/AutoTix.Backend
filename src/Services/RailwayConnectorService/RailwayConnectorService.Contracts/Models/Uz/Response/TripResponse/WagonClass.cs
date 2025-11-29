using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class WagonClass
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("free_seats")]
    public int FreeSeats { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("amenities")]
    public List<string> Amenities { get; set; }
}
