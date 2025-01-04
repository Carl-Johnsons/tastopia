using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record UpdateUserCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public IFormFile Avatar { get; set; } = null!;
    public IFormFile Background { get; set; } = null!;
}
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public UpdateUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.AccountId == request.AccountId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserError.NotFound);
        }

        if (request.DisplayName != null)
        {
            user.DisplayName = request.DisplayName;
        }

        if (request.Bio != null)
        {
            user.Bio = request.Bio;
        }

        if (request.Gender != null)
        {
            user.Gender = request.Gender;
        }

        _context.Users.Update(user);
        await _unitOfWork.SaveChangeAsync();

        return Result.Success();
    }
}
