using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class Passenger
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("ticket_type")]
    public int TicketType { get; set; }

    [JsonProperty("privilege_id")]
    public int PrivilegeId { get; set; }

    [JsonProperty("privilege_data")]
    public PrivilegeData PrivilegeData { get; set; }

    [JsonProperty("privilege")]
    public Privilege Privilege { get; set; }

    [JsonProperty("photo")]
    public object Photo { get; set; }

    [JsonProperty("main")]
    public bool Main { get; set; }

    [JsonProperty("phone")]
    public object Phone { get; set; }

    [JsonProperty("is_share_user")]
    public bool IsShareUser { get; set; }

    [JsonProperty("birthday")]
    public object Birthday { get; set; }

    [JsonProperty("gender")]
    public object Gender { get; set; }
}
