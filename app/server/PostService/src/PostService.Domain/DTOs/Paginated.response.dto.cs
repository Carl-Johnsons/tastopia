using Newtonsoft.Json;
using PostService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

/* ===================== Metadata  ===================== */

public class PostListMetadata
{
}

public class EmptyMetadata
{
}

public class CommonPaginatedMetadata
{
    [Required]
    [JsonProperty("totalPage")]
    public int TotalPage { get; set; } = 0;
}


/* ===================== DTO  ===================== */

public class PaginatedPostListDTO : BasePaginatedDTO
{
}

public class PaginatedCommentListDTO : BasePaginatedDTO
{
    [JsonProperty("postId")]
    public Guid PostId { get; set; }
}

public class PaginatedReportListDTO : BasePaginatedDTO
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
}

/* ===================== Response  ===================== */

public class PaginatedPostListResponseDTO : BasePaginatedResponse<string, PostListMetadata>;
public class PaginatedCommentListResponseDTO : BasePaginatedResponse<Comment, EmptyMetadata>;
public class PaginatedReportListResponseDTO : BasePaginatedResponse<PostReportDTO, CommonPaginatedMetadata>;

