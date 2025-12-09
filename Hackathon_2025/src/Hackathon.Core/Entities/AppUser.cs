using Microsoft.AspNetCore.Identity;

namespace Hackathon.Core.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public Guid? FacePersonId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsFaceIdEnabled { get; set; } = false;
}
