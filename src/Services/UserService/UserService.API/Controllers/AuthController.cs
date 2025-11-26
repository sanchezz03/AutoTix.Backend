using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs.Request;
using UserService.Application.Services.Interfaces;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendSmsRequest request)
    {
        var result = await _service.SendSmsAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _service.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return NoContent();
    }
}
