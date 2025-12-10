using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class Order : BaseEntity
{
    public decimal Price { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}



