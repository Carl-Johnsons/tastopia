using AutoMapper;
using Contract.DTOs;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.Tags.Queries;

public class AdminGetTagsQuery: IRequest<Result<PaginatedAdminTagListResponse?>>
{
    public PaginatedDTO PaginatedDTO { get; set; } = null!;
}

public class AdminGetTagsQueryHandler : IRequestHandler<AdminGetTagsQuery, Result<PaginatedAdminTagListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Tag, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    public AdminGetTagsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Tag, AdvancePaginatedMetadata> paginateDataUtility, IMapper mapper)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedAdminTagListResponse?>> Handle(AdminGetTagsQuery request, CancellationToken cancellationToken)
    {
        var paginatedDto = request.PaginatedDTO;

        if(paginatedDto.Skip == null)
        {
            return Result<PaginatedAdminTagListResponse?>.Failure(TagError.NullParameter, "AccountId or Skip is null.");
        }

        var tagsQuery = _context.Tags.AsQueryable();

        if (!string.IsNullOrEmpty(paginatedDto.Keyword))
        {
            var keyword = paginatedDto.Keyword;
            tagsQuery = tagsQuery.Where(t => t.Value.ToLower().Contains(keyword.ToLower()));
        }
        var totalRow = await tagsQuery.CountAsync();

        var limit = paginatedDto.Limit;
        if(limit == null)
        {
            limit = TAG_CONSTANTS.ADMIN_TAG_LIMIT;
        }
        var totalPage = (totalRow + limit.Value - 1) / limit.Value;

        tagsQuery = _paginateDataUtility.PaginateQuery(tagsQuery, new PaginateParam
        {
            Offset = (paginatedDto.Skip ?? 0) * limit.Value,
            Limit = limit.Value,
            SortBy = !string.IsNullOrEmpty(paginatedDto.SortBy) ? paginatedDto.SortBy : "CreatedAt",
            SortOrder = paginatedDto.SortOrder ?? SortType.DESC,
        });

        var tagList = await tagsQuery.ToListAsync();

        if (tagList == null || !tagList.Any())
        {
            return Result<PaginatedAdminTagListResponse?>.Success(new PaginatedAdminTagListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    TotalPage = 0,
                    TotalRow = 0,
                    CurrentPage = paginatedDto.Skip!.Value + 1
                }
            });

        }
        var result = tagList.Select(t => new AdminTagResponse
        {
            Id = t.Id,
            Code = t.Code,
            Value = t.Value,
            Category = t.Category.ToString(),
            Status = t.Status.ToString(),
            ImageUrl = t.ImageUrl,
            CreatedAt = t.CreatedAt
        }).ToList();
        var paginatedResponse = new PaginatedAdminTagListResponse
        {
            PaginatedData = result,
            Metadata = new NumberedPaginatedMetadata
            {
                TotalPage = totalPage,
                TotalRow = totalRow,
                CurrentPage = paginatedDto.Skip!.Value + 1
            }
        };
        return Result<PaginatedAdminTagListResponse?>.Success(paginatedResponse);
    }
}
