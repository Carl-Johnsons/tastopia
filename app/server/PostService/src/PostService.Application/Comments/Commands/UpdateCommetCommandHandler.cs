using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Comments.Commands;

public class UpdateCommetCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
}

public class UpdateCommetCommandHandler : IRequestHandler<UpdateCommetCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommetCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCommetCommand request, CancellationToken cancellationToken)
    {
        var comment = _context.Comments
                        .Where(p => p.Id == request.Id)
                        .SingleOrDefault();

        if (comment != null)
        {
            comment.Content = request.Content;
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success();
        } else
        {
            return Result.Failure(CommentError.NotFound);
        }

        
    }
}
