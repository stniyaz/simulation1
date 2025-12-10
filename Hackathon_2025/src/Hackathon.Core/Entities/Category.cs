using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}



