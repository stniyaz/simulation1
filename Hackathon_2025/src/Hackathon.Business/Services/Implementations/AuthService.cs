using Hackathon.Business.Dtos.AuthDtos;
using Hackathon.Business.Services.Interfaces;
using Hackathon.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hackathon.Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<TokenDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail)
                ?? await _userManager.FindByNameAsync(dto.UsernameOrEmail);

        if (user == null) throw new Exception("Email və ya şifrə yanlışdır!");

        var checkPass = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!checkPass) throw new Exception("Email və ya şifrə yanlışdır!");

        return await _generateJwtToken(user);
    }

    public Task<TokenDto> LoginByFaceAsync(FaceLoginDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            throw new Exception("Şifrələr uyğun gəlmir!");
        }
        var user = new AppUser
        {
            FullName = dto.FullName,
            UserName = dto.UserName,
            Email = dto.Email,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errors);
        }
    }

    private async Task<TokenDto> _generateJwtToken(AppUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName)
            };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expireDays = Convert.ToDouble(_config["JwtSettings:ExpireDays"]);
        var expireDate = DateTime.Now.AddDays(expireDays);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: expireDate,
            signingCredentials: creds
        );

        return new TokenDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expireDate
        };
    }
}
