namespace UserService.Application.DTOs.Request;

public class LoginRequest
{
    public string Phone { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DeviceInfo Device { get; set; } = null!;
}
