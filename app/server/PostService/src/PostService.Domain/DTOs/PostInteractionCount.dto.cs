namespace PostService.Domain.DTOs;

public class PostInteractionCountDTO
{
    public Guid InteractionId { get; set; }
    public int Count { get; set; }
    public string Value { get; set; } = null!;
}
