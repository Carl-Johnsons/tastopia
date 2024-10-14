using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.DTOs;

namespace PostService.Application.Posts.Queries;

public class GetListReportPostQuery : BasePaginatedDTO, IRequest<Result<PaginatedReportListResponseDTO?>>
{
}
public class GetListReportPostQueryHandler : IRequestHandler<GetListReportPostQuery, Result<PaginatedReportListResponseDTO?>>
{
    private readonly IApplicationDbContext _context;

    public GetListReportPostQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedReportListResponseDTO?>> Handle(GetListReportPostQuery request, CancellationToken cancellationToken)
    {
        var reportPostList = await _context.PostReports
                    .GroupBy(p => p.PostId)
                    .Select(p => new PostReportDTO
                    {
                        PostId = p.Key,
                        PostCount = p.Count()
                    })
                    .OrderByDescending(p => p.PostCount)
                    .ToListAsync(cancellationToken);

        var count = reportPostList.Count();
        var totalPage = count / request.Limit + (count % request.Limit == 0 ? 0 : 1);

        // Group by query do not allow offset and limit so split query is needed
        var posts = reportPostList.Skip(request.Skip * request.Limit).Take(request.Limit);

        var response = new PaginatedReportListResponseDTO
        {
            PaginatedData = posts,
            Metadata = new CommonPaginatedMetadata
            {
                TotalPage = totalPage
            }
        };

        return Result<PaginatedReportListResponseDTO?>.Success(response);
    }
}

