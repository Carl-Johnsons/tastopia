using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Domain.DTOs;

public class DeleteTagDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
