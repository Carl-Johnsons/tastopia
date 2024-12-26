using MassTransit;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Tags;

public class GetTagsCommand : IRequest<Result<PaginatedTagListResponse>>
{
    [Required]
    public int Skip {  get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public List<string>? TagCodes { get; init; }
}

public class GetTagsCommandHandler : IRequestHandler<GetTagsCommand, Result<PaginatedTagListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;
    private readonly IPaginateDataUtility<Tag, CommonPaginatedMetadata> _paginateDataUtility;

    public GetTagsCommandHandler(IApplicationDbContext context, IPaginateDataUtility<Tag, CommonPaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public  async Task<Result<PaginatedTagListResponse>> Handle(GetTagsCommand request, CancellationToken cancellationToken)
    {
        var tagCodes = request.TagCodes;
        var keyword = request.Keyword;
        var skip = request.Skip;

        var tagsQuery = _context.Tags.AsQueryable();

        if(tagCodes != null && tagCodes.Any())
        {
            tagsQuery = tagsQuery.Where(t => tagCodes.Any(tagCode => t.Code == tagCode));
        }

        if(keyword != null && keyword != "")
        {
            tagsQuery = tagsQuery.Where(t => t.Value.ToLower().Contains(keyword.ToLower()));
        }

        var totalPage = (await tagsQuery.CountAsync() + TAG_CONSTANTS.TAG_LIMIT - 1) / TAG_CONSTANTS.TAG_LIMIT;

        tagsQuery = _paginateDataUtility.PaginateQuery(tagsQuery, new PaginateParam
        {
            Offset = skip * TAG_CONSTANTS.TAG_LIMIT,
            Limit = TAG_CONSTANTS.TAG_LIMIT
        });

        var tagList = await tagsQuery.ToListAsync();

        if (tagList == null || !tagList.Any())
        {
            return Result<PaginatedTagListResponse>.Success(new PaginatedTagListResponse
            {
                Metadata = new CommonPaginatedMetadata
                {
                    TotalPage = 0
                },
                PaginatedData = []
            });
        }

        var paginatedResponse = new PaginatedTagListResponse
        {
            PaginatedData = tagList,
            Metadata = new CommonPaginatedMetadata
            {
                TotalPage = totalPage,
            }
        };
        return Result<PaginatedTagListResponse>.Success(paginatedResponse);
    }
}
