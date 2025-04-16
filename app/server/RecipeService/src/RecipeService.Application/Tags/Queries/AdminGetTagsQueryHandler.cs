using Contract.DTOs;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.Tags.Queries;

public class AdminGetTagsQuery: IRequest<Result<PaginatedAdminTagListResponse?>>
{
    public PaginatedDTO PaginatedDTO { get; set; } = null!;
    public string Lang { get; set; } = null!;
}

public class AdminGetTagsQueryHandler : IRequestHandler<AdminGetTagsQuery, Result<PaginatedAdminTagListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminTagResponse, AdvancePaginatedMetadata> _paginateDataUtility;
    public AdminGetTagsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<AdminTagResponse, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public Task<Result<PaginatedAdminTagListResponse?>> Handle(AdminGetTagsQuery request, CancellationToken cancellationToken)
    {
        var paginatedDto = request.PaginatedDTO;
        var lang = request.Lang;

        if(paginatedDto.Skip == null || string.IsNullOrEmpty(lang))
        {
            return Task.FromResult(Result<PaginatedAdminTagListResponse?>.Failure(TagError.NullParameter, "Skip or Language is null."));
        }

        var tagsQuery = _context.Tags.AsEnumerable().Select(t => 
        new AdminTagResponse
        {
            Id = t.Id,
            Category = TAG_CONSTANTS.GetTagCategoryTranslate(t.Category, lang),
            Code = t.Code,
            CreatedAt = t.CreatedAt,
            En = t.Value.En,
            Vi = t.Value.Vi,
            ImageUrl = t.ImageUrl,
            Status = t.Status.ToString(),
        }).AsQueryable();

        if (!string.IsNullOrEmpty(paginatedDto.Keyword))
        {
            var keyword = paginatedDto.Keyword.ToLower();
            tagsQuery = tagsQuery.Where(t => t.En.ToLower().Contains(keyword) || t.Vi.ToLower().Contains(keyword));
        }
        var totalRow = tagsQuery.Count();

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

        var tagList = tagsQuery.ToList();

        if (tagList == null || !tagList.Any())
        {
            return Task.FromResult(Result<PaginatedAdminTagListResponse?>.Success(new PaginatedAdminTagListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    TotalPage = 0,
                    TotalRow = 0,
                    CurrentPage = paginatedDto.Skip!.Value + 1
                }
            }));

        }
        var paginatedResponse = new PaginatedAdminTagListResponse
        {
            PaginatedData = tagList,
            Metadata = new NumberedPaginatedMetadata
            {
                TotalPage = totalPage,
                TotalRow = totalRow,
                CurrentPage = paginatedDto.Skip!.Value + 1
            }
        };
        return Task.FromResult(Result<PaginatedAdminTagListResponse?>.Success(paginatedResponse));
    }
}
