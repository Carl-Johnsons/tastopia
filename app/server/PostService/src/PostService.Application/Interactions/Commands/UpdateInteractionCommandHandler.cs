using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Interactions.Commands;

public class UpdateInteractionCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Value { get; init; } = null!;
}

public class UpdateInteractionCommandHandler : IRequestHandler<UpdateInteractionCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInteractionCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateInteractionCommand request, CancellationToken cancellationToken)
    {
        var interaction = _context.Interactions
                            .Where(it => it.Id == request.Id)
                            .SingleOrDefault();

        if (interaction == null)
        {
            return Result.Failure(InteractionError.NotFound);
        } 
        else
        {
            interaction.Value = request.Value;
            interaction.Code = request.Value.ToUpper();
            _context.Interactions.Add(interaction);

            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }          

    }
}
