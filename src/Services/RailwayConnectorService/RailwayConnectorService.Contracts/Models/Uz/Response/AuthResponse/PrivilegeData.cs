using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class PrivilegeData
{
    [JsonProperty("student_id")]
    public string StudentId { get; set; }
}