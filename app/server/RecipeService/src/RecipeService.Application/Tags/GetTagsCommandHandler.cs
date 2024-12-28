using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Tags;

public class GetTagsCommand : IRequest<Result<PaginatedTagListResponse?>>
{
    [Required]
    public int? Skip {  get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public List<string>? TagCodes { get; init; }

    [Required]
    public string Category { get; init; } = null!;
}

public class GetTagsCommandHandler : IRequestHandler<GetTagsCommand, Result<PaginatedTagListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Tag, AdvancePaginatedMetadata> _paginateDataUtility;

    public GetTagsCommandHandler(IApplicationDbContext context, IPaginateDataUtility<Tag, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public  async Task<Result<PaginatedTagListResponse?>> Handle(GetTagsCommand request, CancellationToken cancellationToken)
    {
        var tagCodes = request.TagCodes;
        var keyword = request.Keyword;
        var category = request.Category;
        var skip = request.Skip;

        if(skip == null || tagCodes == null || !tagCodes.Any() || category == null) {
            return Result<PaginatedTagListResponse?>.Failure(TagError.NotFound);
        }

        tagCodes.RemoveAll(string.IsNullOrEmpty);

        if (!tagCodes.Any())
        {
            return Result<PaginatedTagListResponse>.Failure(TagError.NotFound);
        }

        var tagsQuery = _context.Tags.OrderByDescending(t => t.CreatedAt).AsQueryable();

        if(!string.IsNullOrEmpty(category) && category != "ALL")
        {
            tagsQuery = tagsQuery.Where(t => t.Category == category);
        }

        if(!tagCodes.Contains("ALL"))
        {
            tagsQuery = tagsQuery.Where(t => tagCodes.Any(tagCode => t.Code == tagCode));
        }

        if(!string.IsNullOrEmpty(keyword))
        {
            tagsQuery = tagsQuery.Where(t => t.Value.ToLower().Contains(keyword.ToLower()));
        }

        var totalPage = (await tagsQuery.CountAsync() + TAG_CONSTANTS.TAG_LIMIT - 1) / TAG_CONSTANTS.TAG_LIMIT;

        tagsQuery = _paginateDataUtility.PaginateQuery(tagsQuery, new PaginateParam
        {
            Offset = skip ?? 0 * TAG_CONSTANTS.TAG_LIMIT,
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
        if(skip >= totalPage - 1)
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
