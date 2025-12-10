using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class OrderItem : BaseEntity
{
    public float Quantity { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}



