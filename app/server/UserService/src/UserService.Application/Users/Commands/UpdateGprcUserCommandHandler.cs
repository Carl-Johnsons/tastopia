using AccountProto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UploadFileProto;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record UpdateGprcUserCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; init; }
    public string? DisplayName { get; init; } = null!;
    public string? Gender { get; init; } = null!;
    public string? Username { get; init; } = null!;
    public DateTime? Dob { get; init; } = null!;
    public string? AvatarUrl { get; init; } = null!;
    public string? Address { get; init; } = null!;
    public bool IsDobUpdate { get; init; } = false;
}
public class UpdateGprcUserCommandHandler : IRequestHandler<UpdateGprcUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;

    public UpdateGprcUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient, GrpcAccount.GrpcAccountClient grpcAccountClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _grpcUploadFileClient = grpcUploadFileClient;
        _grpcAccountClient = grpcAccountClient;
    }

    public async Task<Result> Handle(UpdateGprcUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.AccountId == request.AccountId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserError.NotFound);
        }

        if (!string.IsNullOrEmpty(request.DisplayName))
        {
            user.DisplayName = request.DisplayName;
        }

        if (!string.IsNullOrEmpty(request.Address))
        {
            user.Address = request.Address;
        }

        if (request.IsDobUpdate)
        {
            user.Dob = request.Dob;
        }

        if (!string.IsNullOrEmpty(request.AvatarUrl))
        {
            user.AvatarUrl = request.AvatarUrl;
        }

        if (!string.IsNullOrEmpty(request.Gender))
        {
            if (Enum.IsDefined(typeof(GenderType), request.Gender))
            {
                user.Gender = request.Gender;
            }
            else
            {
                return Result.Failure(UserError.NullParameters, "Gender must be MALE or FEMALE");
            }
        }

        if (!string.IsNullOrEmpty(request.Username))
        {
            var existUser = await _context.Users.SingleOrDefaultAsync(u => u.AccountUsername == request.Username && u.AccountId != request.AccountId);
            if (existUser != null)
            {
                return Result.Failure(UserError.AlreadyExistUser, "Already exist user");
            }
            user.AccountUsername = request.Username;
        }

        _context.Users.Update(user);
        await _unitOfWork.SaveChangeAsync();

        return Result.Success();
    }
}
