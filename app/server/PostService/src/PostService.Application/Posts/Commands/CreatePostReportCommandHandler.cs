using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Constants;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Commands;

public class CreatePostReportCommand : IRequest<Result>
{
    public Guid PostId { get; init; }
    public Guid UserId { get; init; }
    public string Reason { get; init; } = null!;
}

public class CreatePostReportCommandHandler : IRequestHandler<CreatePostReportCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISignalRService _signalRService;

    public CreatePostReportCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ISignalRService signalRService)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _signalRService = signalRService;
    }

    public async Task<Result> Handle(CreatePostReportCommand request, CancellationToken cancellationToken)
    {
        var post = _context.Posts
                    .Where(p => p.Id == request.PostId)
                    .SingleOrDefault();

        if (post == null)
        {
            return Result.Failure(PostError.NotFound);
        }
        var postReport = _context.PostReports
                            .Where(rp => rp.PostId == request.PostId && rp.UserId == request.UserId)
                            .SingleOrDefault();
        if (postReport != null)
        {
            return Result.Failure(PostError.AlreadyReportedPost);
        }

        var rp = new PostReport
        {
            PostId = request.PostId,
            UserId = request.UserId,
            Reason = request.Reason,
        };        

        var warning = _context.UserWarnings
                        .Where(w => w.UserId == rp.UserId)
                        .SingleOrDefault();

        if (warning != null)
        {
            warning.WarningCount += 1;
        } else
        {
            _context.UserWarnings.Add(new UserWarning
            {
                UserId = rp.UserId,
                WarningCount = 1,
            });
        }

        _context.PostReports.Add(rp);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        await _signalRService.InvokeAction(SignalREvent.REPORT_POST);

        return Result.Success();
    }
}