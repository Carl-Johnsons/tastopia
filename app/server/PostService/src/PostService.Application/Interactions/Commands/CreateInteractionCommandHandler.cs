using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Interactions.Commands;

public class CreateInteractionCommand : IRequest<Result>
{
    public string Value { get; init; } = null!;
    public string Gif { get; init; } = null!;
    public string Code { get; init; } = null!;

}

public class CreateInteractionCommandHandler : IRequestHandler<CreateInteractionCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInteractionCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateInteractionCommand request, CancellationToken cancellationToken)
    {

        var interaction = _context.Interactions
                .Where(i => i.Value == request.Value.Trim())
                .FirstOrDefault();

        if (interaction != null)
        {
            return Result.Failure(InteractionError.AlreadyExited);
        }

        Interaction result = new Interaction 
        {
            Value = request.Value.Trim(),
            Code = request.Code.ToUpper().Trim(),
            Gif = request.Gif.Trim(),
        }; 

        _context.Interactions.Add(result);
        await _unitOfWork.SaveChangeAsync();

        return Result.Success();
    }
}
