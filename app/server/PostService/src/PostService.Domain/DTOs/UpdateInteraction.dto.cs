using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Domain.DTOs;

public class UpdateInteractionDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;
}
