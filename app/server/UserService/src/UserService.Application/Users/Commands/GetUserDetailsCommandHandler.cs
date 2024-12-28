using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record GetUserDetailsCommand : IRequest<Result<User?>>
{
    [Required]
    public Guid UserId { get; init; }
}
public class GetUserDetailsCommandHandler : IRequestHandler<GetUserDetailsCommand, Result<User?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserDetailsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
    }

    public async Task<Result<User?>> Handle(GetUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var user = await _context.Users
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();
        if (user == null) { 
            return Result<User?>.Failure(UserError.NotFound);
        }
        return Result<User?>.Success(user);
    }
}

