using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Tags.Commands;

public class DeleteTagCommand : IRequest<Result>
{
    public Guid TagId { get; init; }
}

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _context.Tags
                    .Where(t => t.Id == request.TagId)
                    .FirstOrDefault();
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success();
        }
        else
        {
            return Result.Failure(TagError.NotFound);
        }
        
    }
}
