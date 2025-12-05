using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class Wagon
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("number")]
    public string Number { get; set; }

    [JsonProperty("mockup_name")]
    public string MockupName { get; set; }

    [JsonProperty("seats")]
    public List<int> Seats { get; set; }

    [JsonProperty("free_seats_top")]
    public int FreeSeatsTop { get; set; }

    [JsonProperty("free_seats_lower")]
    public int FreeSeatsLower { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("air_conditioner")]
    public bool AirConditioner { get; set; }

    [JsonProperty("services")]
    public List<Service> Services { get; set; }

    [JsonProperty("privileges")]
    public List<Privilege> Privileges { get; set; }
}
