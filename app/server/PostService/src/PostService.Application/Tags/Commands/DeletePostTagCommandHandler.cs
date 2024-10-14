using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace PostService.Application.Tags.Commands;

public class DeletePostTagCommand : IRequest
{
    public Guid PostId { get; init; }
}

public class DeletePostTagCommandHandler : IRequestHandler<DeletePostTagCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeletePostTagCommand request, CancellationToken cancellationToken)
    {
        var postTags = _context.PostTags
            .Where(pt => pt.PostId == request.PostId)
            .ToList();

        if (!postTags.IsNullOrEmpty())
        {
            foreach (var postTag in postTags)
            {
                await Console.Out.WriteLineAsync("======" + postTag.TagId.ToString());
                _context.PostTags.Remove(postTag);
            }

        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}