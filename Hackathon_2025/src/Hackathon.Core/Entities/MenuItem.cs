using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }


    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}



