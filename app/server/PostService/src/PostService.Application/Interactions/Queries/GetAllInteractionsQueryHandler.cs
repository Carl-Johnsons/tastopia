using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;

namespace PostService.Application.Interactions.Queries;

public class GetAllInteractionsQuery : IRequest<Result<List<Interaction>>>;

public class GetAllInteractionsQueryHandler : IRequestHandler<GetAllInteractionsQuery, Result<List<Interaction>>>
{
    private readonly IApplicationDbContext _context;
    public GetAllInteractionsQueryHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
    }

    public async Task<Result<List<Interaction>>> Handle(GetAllInteractionsQuery request, CancellationToken cancellationToken)
    {
        var interactions = await _context.Interactions.OrderBy(i => i.CreatedAt).ToListAsync(cancellationToken);

        return Result<List<Interaction>>.Success(interactions);
    }
}
