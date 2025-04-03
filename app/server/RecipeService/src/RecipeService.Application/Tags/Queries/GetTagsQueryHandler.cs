using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Tags.Queries;

public class GetTagsQuery : IRequest<Result<PaginatedTagListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public List<string>? TagCodes { get; init; }

    [Required]
    public string Category { get; init; } = null!;

    [Required]
    public string Lang { get; init; } = null!;
}

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, Result<PaginatedTagListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Tag, AdvancePaginatedMetadata> _paginateDataUtility;

    public GetTagsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Tag, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedTagListResponse?>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tagCodes = request.TagCodes;
        var keyword = request.Keyword;
        var category = request.Category;
        var skip = request.Skip;
        var lang = request.Lang;

        if (skip == null || tagCodes == null || category == null || lang == null)
        {
            return Result<PaginatedTagListResponse?>.Failure(TagError.NullParameter);
        }

        tagCodes.RemoveAll(string.IsNullOrEmpty);

        var tagsQuery = _context.Tags.Where(t => t.Status == TagStatus.Active).OrderByDescending(t => t.CreatedAt).AsQueryable();

        if (!string.IsNullOrEmpty(category) && category.ToLower() != TagCategory.All.ToString().ToLower())
        {
            tagsQuery = tagsQuery.Where(t => t.Category == (TagCategory)Enum.Parse(typeof(TagCategory), category));
        }

        if (!tagCodes.Contains("ALL") && tagCodes.Count != 0)
        {
            tagsQuery = tagsQuery.Where(t => tagCodes.Any(tagCode => t.Code == tagCode));
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            tagsQuery = tagsQuery.Where(t => t.Value.En.ToLower().Contains(keyword) || t.Value.Vi.ToLower().Contains(keyword));
        }

        var totalPage = (await tagsQuery.CountAsync() + TAG_CONSTANTS.TAG_LIMIT - 1) / TAG_CONSTANTS.TAG_LIMIT;

        tagsQuery = _paginateDataUtility.PaginateQuery(tagsQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * TAG_CONSTANTS.TAG_LIMIT,
            Limit = TAG_CONSTANTS.TAG_LIMIT
        });

        var tagList = await tagsQuery.ToListAsync();

        if (tagList == null || !tagList.Any())
        {
            return Result<PaginatedTagListResponse?>.Success(new PaginatedTagListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    TotalPage = 0,
                    HasNextPage = false,
                }
            });

        }

        var hasNextPage = true;
        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var paginatedResponse = new PaginatedTagListResponse
        {
            PaginatedData = tagList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedTagListResponse?>.Success(paginatedResponse);
    }
}
