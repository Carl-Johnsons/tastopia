using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Commands;

public record DeleteUserContentRestrictionsCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteUserContentRestrictionsCommandHandler : IRequestHandler<DeleteUserContentRestrictionsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserContentRestrictionsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserContentRestrictionsCommand request, CancellationToken cancellationToken)
    {
        var contentRestrictions = _context.UserContentRestrictions
                            .Where(u => u.Id == request.Id)
                            .SingleOrDefault();

        if (contentRestrictions == null)
        {
            return Result.Failure(ContentRestrictionsError.NotFound);
        }
        else
        {
            _context.UserContentRestrictions.Remove(contentRestrictions);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success();
        }
    }
}
