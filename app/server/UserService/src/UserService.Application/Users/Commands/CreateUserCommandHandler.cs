using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record CreateUserCommand : IRequest<Result>
{
    [Required]
    public User User { get; init; } = null!;
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.FirstOrDefault(us => us.AccountId == request.User.AccountId);

        if(user != null) {
            return Result.Failure(UserError.AlreadyExistUser);
        }

        user = new User {
            AccountId = request.User.AccountId,
            AvatarUrl = request.User.AvatarUrl,
            BackgroundUrl = request.User.BackgroundUrl,
            Gender = request.User.Gender,
            Dob = request.User.Dob,
        };

        _context.Users.Add(user);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Result.Success();
    }
}

