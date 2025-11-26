namespace RailwayConnectorService.Contracts.Models.Uz.Request.Auth;

public class DeviceInfo
{
    public string Name { get; set; } = null!;
    public string? FcmToken { get; set; }
}
