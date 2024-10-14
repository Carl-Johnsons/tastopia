using System.ComponentModel.DataAnnotations.Schema;

namespace UploadFileService.Domain.Common;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
