using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Common;

public class BaseMongoDBAuditableEntity : BaseMongoDBEntity
{
    [Column]
    public DateTime CreatedAt { get; set; }
    [Column]
    public DateTime UpdatedAt { get; set; }
}
