using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Interactions.Commands;

public class DeleteInteractionCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

public class DeleteInteractionCommandHandler : IRequestHandler<DeleteInteractionCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInteractionCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteInteractionCommand request, CancellationToken cancellationToken)
    {
        var interaction = _context.Interactions
                            .Where(it => it.Id == request.Id)
                            .SingleOrDefault();

        if (interaction == null)
        {
            return Result.Failure(InteractionError.NotFound);
        }
        
        _context.Interactions.Remove(interaction);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
