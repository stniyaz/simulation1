using Microsoft.AspNetCore.Http;

namespace Hackathon.Business.Dtos.AuthDtos;

public class FaceLoginDto
{
    public IFormFile Photo { get; set; }
}
