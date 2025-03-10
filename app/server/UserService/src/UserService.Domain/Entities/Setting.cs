using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("Setting")]
public class Setting : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    [JsonConverter(typeof(StringEnumConverter))]
    public SettingDataType DataType { get; set; }
    public string DefaultValue { get; set; } = null!;
}
