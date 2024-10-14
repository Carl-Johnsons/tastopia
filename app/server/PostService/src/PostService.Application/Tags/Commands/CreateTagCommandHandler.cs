using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Tags.Commands;

public class CreateTagCommand : IRequest<Result>
{
    public string Value { get; init; } = null!;

}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _context.Tags
                .Where(t =>  t.Value == request.Value.Trim())
                .FirstOrDefault();

        if (tag == null)
        {
            var tags = new Tag
            {
                Value = request.Value.Trim(),
                Code = request.Value.ToUpper().Trim()
            };
            _context.Tags.Add(tags);
            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        } else
        {
            return Result.Failure(TagError.AlreadyExited);
        }   
    }
}
