using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.TripResponse;

public class Monitoring
{
    [JsonProperty("allowed")]
    public bool Allowed { get; set; }

    [JsonProperty("auto_purchase")]
    public bool AutoPurchase { get; set; }
}
