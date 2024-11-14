using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

/* ===================== Metadata  ===================== */

public class EmptyMetadata { }
public class CommonPaginatedMetadata
{
    [Required]
    [JsonProperty("totalPage")]
    public int TotalPage { get; set; } = 0;
}

/* ===================== DTO  ===================== */

public class PaginatedUserListDTO : BasePaginatedDTO { }


/* ===================== Response  ===================== */

public class PaginatedUserListResponseDTO : BasePaginatedResponse<ApplicationUserResponseDTO, CommonPaginatedMetadata> { }