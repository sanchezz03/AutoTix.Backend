using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class SendSms
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("retry_after")]
    public int RetryAfter { get; set; }
}
