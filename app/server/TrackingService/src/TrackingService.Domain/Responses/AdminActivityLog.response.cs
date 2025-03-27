using Contract.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrackingService.Domain.Responses;

public class AdminActivityLogResponse
{
    public Guid AccountId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ActivityType ActivityType { get; set; }
    public Guid EntityId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ActivityEntityType EntityType { get; set; }
    public Guid? SecondaryEntityId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ActivityEntityType? SecondaryEntityType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class RecipeAdminActivityLogResponse : AdminActivityLogResponse
{
    public RecipeLogResponse Recipe { get; set; } = null!;
}

public class CommentAdminActivityLogResponse : AdminActivityLogResponse
{
    public RecipeLogResponse Recipe { get; set; } = null!;
    public CommentLogResponse Comment { get; set; } = null!;
}


public class RecipeLogResponse
{
    public string? RecipeTitle { get; set; }
    public Guid? RecipeAuthorId { get; set; }
    public string? RecipeAuthorDisplayName { get; set; }
    public string? RecipeImageURL { get; set; }
    public DateTime? RecipeCreatedAt { get; set; }
    public DateTime? RecipeUpdatedAt { get; set; }
    public int? RecipeVoteDiff { get; set; }
}

public class CommentLogResponse
{
    public string AuthorAvatarURL { get; set; } = null!;
    public Guid? AuthorId { get; set; }
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorUsername { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
