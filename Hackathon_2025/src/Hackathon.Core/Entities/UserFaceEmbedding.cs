using Hackathon.Core.Entities.Common;

namespace Hackathon.Core.Entities;

public class UserFaceEmbedding : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    // 512 float embedding ArcFace üçün
    public float[] Embedding { get; set; } = null!;

}



