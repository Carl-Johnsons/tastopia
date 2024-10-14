using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Queries;

public record GetUserContentRestrictionsByUserIdQuery : IRequest<Result<UserContentRestrictions?>>
{
    public Guid UserId { get; init; }
}

public class GetUserContentRestrictionsByUserIdQueryHandler : IRequestHandler<GetUserContentRestrictionsByUserIdQuery, Result<UserContentRestrictions?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserContentRestrictionsByUserIdQueryHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
    }

    public async Task<Result<UserContentRestrictions?>> Handle(GetUserContentRestrictionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = _context.UserContentRestrictions
                    .Where(u => u.UserId == request.UserId)
                    .SingleOrDefault();

        if (userResult == null)
        {
            return Result<UserContentRestrictions?>.Failure(ContentRestrictionsError.NotFound)!;
        } 
        return Result<UserContentRestrictions?>.Success(userResult);
        
    }
}
