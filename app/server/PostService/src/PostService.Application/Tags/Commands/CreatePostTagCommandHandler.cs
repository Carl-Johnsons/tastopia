using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Tags.Commands;

public class CreatePostTagCommand : IRequest<Result>
{
    public Guid PostId { get; init; }
    public Guid TagId { get; init; }
}

public class CreatePostTagCommandHandler : IRequestHandler<CreatePostTagCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreatePostTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _context.Tags
                        .Where(p => p.Id == request.TagId)
                        .SingleOrDefault();

        if (tag == null)
        {
            return Result.Failure(TagError.NotFound);
        }

        _context.PostTags.Add(new PostTag
        {
            PostId = request.PostId,
            TagId = request.TagId
        });

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
