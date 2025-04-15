using Contract.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrackingService.Domain.Responses;

public class AdminActivityLogResponse
{
    public Guid AccountId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ActivityType ActivityType { get; set; }
    public string AvatarUrl { get; set; } = null!;
    public string? AccountUsername { get; set; } = null!;
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

public class UserReportAdminActivityLogResponse : AdminActivityLogResponse
{
    public UserLogResponse User { get; set; } = null!;
    public UserLogResponse Reporter { get; set; } = null!;
    public ReportLogResponse Report { get; set; } = null!;
}

public class CommentReportAdminActivityLogResponse : AdminActivityLogResponse
{
    public UserLogResponse Reporter { get; set; } = null!;
    public RecipeLogResponse Recipe { get; set; } = null!;
    public CommentLogResponse Comment { get; set; } = null!;
    public ReportLogResponse Report { get; set; } = null!;
}

public class RecipeReportAdminActivityLogResponse : AdminActivityLogResponse
{
    public UserLogResponse Reporter { get; set; } = null!;
    public RecipeLogResponse Recipe { get; set; } = null!;
    public ReportLogResponse Report { get; set; } = null!;
}

public class TagAdminActivityLogResponse : AdminActivityLogResponse
{
    public TagLogResponse Tag { get; set; } = null!;
}
public class RecipeLogResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public Guid? AuthorId { get; set; }
    public string AuthorUsername { get; set; } = null!;
    public string? AuthorDisplayName { get; set; }
    public string? ImageURL { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? VoteDiff { get; set; }
}

public class CommentLogResponse
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public string AuthorAvatarURL { get; set; } = null!;
    public Guid? AuthorId { get; set; }
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorUsername { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class TagLogResponse
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}

public class UserLogResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string AvatarURL { get; set; } = null!;
}

public class UserAdminActivityLogResponse : AdminActivityLogResponse
{
    public UserLogResponse User { get; set; } = null!;
}
public class ReportLogResponse
{
    public Guid Id { get; set; }
    public Guid ReporterAccountId { get; set; }
    public List<string> Reasons { get; set; } = [];
    public string? AdditionalDetail { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}