using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Common;
public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
