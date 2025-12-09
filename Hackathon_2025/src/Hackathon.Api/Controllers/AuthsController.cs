using Hackathon.Business.Dtos.AuthDtos;
using Hackathon.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        await _authService.RegisterAsync(dto);
        return Ok(new { message = "Qeydiyyat uğurla tamamlandı!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var tokenDto = await _authService.LoginAsync(dto);
        return Ok(tokenDto);
    }

}
