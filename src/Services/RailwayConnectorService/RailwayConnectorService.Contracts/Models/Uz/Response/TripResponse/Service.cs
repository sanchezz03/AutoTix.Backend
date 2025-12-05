using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Service
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("select_type")]
    public string SelectType { get; set; }

    [JsonProperty("select_units_max")]
    public object SelectUnitsMax { get; set; }

    [JsonProperty("selected_by_default")]
    public bool SelectedByDefault { get; set; }
}
