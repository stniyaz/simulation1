namespace Hackathon.Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    public DateTime? UpdatedDate { get; set; }
}
