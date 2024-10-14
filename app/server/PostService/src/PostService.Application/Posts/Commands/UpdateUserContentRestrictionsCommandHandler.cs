using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Entities;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Commands;

public record UpdateUserContentRestrictionsCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid TypeId { get; init; }
    public DateTime ExpiredAt { get; init; }
}

public class UpdateUserContentRestrictionsCommandHandler : IRequestHandler<UpdateUserContentRestrictionsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserContentRestrictionsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserContentRestrictionsCommand request, CancellationToken cancellationToken)
    {
        var contentRestrictions = _context.UserContentRestrictions
                            .Where(u => u.Id == request.Id)
                            .SingleOrDefault();

        if (contentRestrictions == null)
        {
            return Result.Failure(ContentRestrictionsError.NotFound);
        } else
        {
            contentRestrictions.UserId = request.UserId;
            contentRestrictions.UserContentRestrictionsTypeId = request.TypeId;
            contentRestrictions.ExpiredAt = request.ExpiredAt;

            _context.UserContentRestrictions.Update(contentRestrictions);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return Result.Success();
        }        
    }
}
