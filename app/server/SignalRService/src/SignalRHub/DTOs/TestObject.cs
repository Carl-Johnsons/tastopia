using System.ComponentModel.DataAnnotations;

namespace SignalRHub.DTOs;

public class TestObject
{
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public DateTime Time { get; set; } = DateTime.Now;
}
