using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using UserProto;
using static UserProto.GrpcUser;
namespace RecipeService.Application.Tags.Queries;
public class GetTagDetailQuery: IRequest<Result<Tag?>>
{
    public Guid AccountId { get; set; }
    public Guid TagId { get; set; }
}

public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, Result<Tag?>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetTagDetailQueryHandler(IApplicationDbContext context, GrpcUserClient grpcUserClient)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<Tag?>> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var tagId = request.TagId;
        if(accountId == Guid.Empty || tagId == Guid.Empty)
        {
            return Result<Tag?>.Failure(TagError.NullParameter, "AccountId or TagId is null.");
        }
        var adminResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString(),
        }, cancellationToken: cancellationToken);
        if (adminResponse == null || !adminResponse.IsAdmin)
        {
            return Result<Tag?>.Failure(TagError.PermissionDeny);
        }
        var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Id == tagId);
        if (tag == null)
        {
            return Result<Tag?>.Failure(TagError.NotFound, "Not found tag.");
        }
        return Result<Tag?>.Success(tag);
    }
}
