namespace Hackathon.Business.Dtos.Category;

public record CategoryUpdateDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
