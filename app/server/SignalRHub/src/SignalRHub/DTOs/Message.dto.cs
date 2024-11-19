using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SignalRHub.DTOs;

public class MessageDTO : BaseAuditableEntity
{
    [AllowNull]
    public Guid? SenderId { get; set; } // This props is null if the message come from the system
    [Required]
    public Guid ConversationId { get; set; } // This props is the receiver
    public string Content { get; set; } = "";

    [Required]
    [MaxLength(20)]
    public string Source { get; set; } = null!;

    [AllowNull]
    public string? AttachedFilesURL { get; set; } = null!;
    [AllowNull]
    public bool? Active { get; set; }
}
