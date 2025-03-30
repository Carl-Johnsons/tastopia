using AccountProto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public class GetAdminDetailQuery : IRequest<Result<AdminDetailResponse?>>
{
    [Required]
    public Guid AccountId { get; init; }
}

public class GetAdminDetailQueryHandler : IRequestHandler<GetAdminDetailQuery, Result<AdminDetailResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;

    public GetAdminDetailQueryHandler(IApplicationDbContext context,
        IMapper mapper,
        GrpcAccount.GrpcAccountClient grpcAccountClient)
    {
        _context = context;
        _mapper = mapper;
        _grpcAccountClient = grpcAccountClient;
    }

    public async Task<Result<AdminDetailResponse?>> Handle(GetAdminDetailQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;

        var user = await _context.Users
         .Where(user => user.AccountId == accountId)
         .FirstOrDefaultAsync();

        if (user == null)
        {
            return Result<AdminDetailResponse?>.Failure(UserError.NotFound);
        }

        var grpcRequest = new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString()
        };

        var grpcResponse = await _grpcAccountClient.GetAccountDetailAsync(grpcRequest, cancellationToken: cancellationToken);

        if (grpcResponse == null)
        {
            return Result<AdminDetailResponse>.Failure(UserError.NotFound);
        }

        var result = new AdminDetailResponse
        {
            AccountId = user.AccountId,
            Address = user.Address,
            AvatarUrl = user.AvatarUrl,
            Dob = user.Dob,
            DisplayName = user.DisplayName,
            Email = grpcResponse.Email,
            PhoneNumber = grpcResponse.PhoneNumber,
            Gender = user.Gender,
            IsActive = grpcResponse.IsActive,
            Username = grpcResponse.UserName,
            CreatedAt = grpcResponse.CreatedAt.ToDateTime(),
            UpdatedAt = grpcResponse.UpdatedAt.ToDateTime()
        };

        return Result<AdminDetailResponse?>.Success(result);
    }
}
