using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class Profile
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("email")]
    public object Email { get; set; }

    [JsonProperty("share_text")]
    public string ShareText { get; set; }

    [JsonProperty("passenger")]
    public Passenger Passenger { get; set; }

    [JsonProperty("additional_actions")]
    public List<AdditionalAction> AdditionalActions { get; set; }

    [JsonProperty("loyalty")]
    public Loyalty Loyalty { get; set; }

    [JsonProperty("diia_verified_at")]
    public object DiiaVerifiedAt { get; set; }

    [JsonProperty("verified_at")]
    public object VerifiedAt { get; set; }

    [JsonProperty("achievements")]
    public Achievements Achievements { get; set; }

    [JsonProperty("genders")]
    public List<Gender> Genders { get; set; }

    [JsonProperty("special_statuses")]
    public List<object> SpecialStatuses { get; set; }

    [JsonProperty("verification_type")]
    public object VerificationType { get; set; }

    [JsonProperty("available_verification_types")]
    public List<int> AvailableVerificationTypes { get; set; }
}
