using Microsoft.AspNetCore.Identity;

namespace Hackathon.Core.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    //public Guid? FacePersonId { get; set; }
    public string? ProfileImageUrl { get; set; }
    //public bool IsFaceIdEnabled { get; set; } = false;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<UserFaceEmbedding> UserFaceEmbeddings { get; set; } = new List<UserFaceEmbedding>();


}
