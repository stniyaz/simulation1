namespace Hackathon.Business.Dtos.AuthDtos;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}
