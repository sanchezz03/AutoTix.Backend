using Newtonsoft.Json;

namespace UserService.Infrastructure.External.RailwayConnector.Models.AuthResponse;

public class PrivilegeData
{
    [JsonProperty("student_id")]
    public string StudentId { get; set; }
}