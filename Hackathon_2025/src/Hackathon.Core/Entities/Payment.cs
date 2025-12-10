using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;

    //public string SourceType { get; set; } = null!;// PaymentSourceType
    //public Guid? SourceId { get; set; }
    public string ReceptId { get; set; } = null!;
    public string SecretId { get; set; } = null!;
    public string Status { get; set; } = PaymentStatus.Pending.ToString();
    public string ConfirmToken { get; set; } = Guid.NewGuid().ToString();


    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}



