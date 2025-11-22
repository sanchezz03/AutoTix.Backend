using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class PrivilegeData
{
    [JsonProperty("student_id")]
    public string StudentId { get; set; }
}