using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Tags.Commands;

public class UpdateTagCommand : IRequest<Result>
{

    public Guid TagId { get; init; }
    public string Value { get; init; } = null!;

}

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _context.Tags
                    .Where(c => c.Id == request.TagId)
                    .SingleOrDefault();

        if (tag != null)
        {
            tag.Value = request.Value;
            tag.Code = request.Value.ToUpper();

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success();
        }
        else
        {
            return Result.Failure(TagError.NotFound);
        }

       
    }

}
