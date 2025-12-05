using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class Monitoring
{
    [JsonProperty("allowed")]
    public bool Allowed { get; set; }

    [JsonProperty("auto_purchase")]
    public bool AutoPurchase { get; set; }
}
