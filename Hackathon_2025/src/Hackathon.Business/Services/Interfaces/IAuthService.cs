using Hackathon.Business.Dtos.AuthDtos;

namespace Hackathon.Business.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<TokenDto> LoginAsync(LoginDto dto);
    Task<TokenDto> LoginByFaceAsync(FaceLoginDto dto);
}
