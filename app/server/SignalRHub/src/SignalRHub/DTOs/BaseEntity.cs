using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRHub.DTOs;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
