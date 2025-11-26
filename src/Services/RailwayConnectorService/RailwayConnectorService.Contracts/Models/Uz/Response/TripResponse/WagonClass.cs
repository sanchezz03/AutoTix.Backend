using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class WagonClass
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("name")]
    public string Name;

    [JsonProperty("free_seats")]
    public int FreeSeats;

    [JsonProperty("price")]
    public int Price;

    [JsonProperty("amenities")]
    public List<string> Amenities;
}
