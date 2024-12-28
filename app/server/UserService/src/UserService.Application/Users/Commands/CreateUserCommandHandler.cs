using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record CreateUserCommand : IRequest<Result<User?>>
{
    [Required]
    public User User { get; init; } = null!;
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.FirstOrDefault(us => us.Id == request.User.Id);

        if(user != null) {
            return Result<User?>.Failure(UserError.AlreadyExistUser);
        }

        user = new User {
            Id = request.User.Id,
            AvatarUrl = request.User.AvatarUrl,
            BackgroundUrl = request.User.BackgroundUrl,
            Gender = request.User.Gender,
            Dob = request.User.Dob,
            Address = request.User.Address,
            DisplayName = request.User.DisplayName,
            Bio = request.User.Bio,
        };

        _context.Users.Add(user);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Result<User?>.Success(user);
    }
}

