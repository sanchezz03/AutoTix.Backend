namespace UserService.Application.DTOs.Request;

public class DeviceInfo
{
    public string Name { get; set; } = null!;
    public string? FcmToken { get; set; }
}
