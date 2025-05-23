﻿using AccountProto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public record GetUserDetailsQuery : IRequest<Result<GetUserDetailsResponse?>>
{
    [Required]
    public Guid AccountId { get; init; }

    [Required]
    public Guid? CurrentAccountId { get; init; }

}
public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, Result<GetUserDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;

    public GetUserDetailsQueryHandler(IApplicationDbContext context,
        IMapper mapper,
        GrpcAccount.GrpcAccountClient grpcAccountClient)
    {
        _context = context;
        _mapper = mapper;
        _grpcAccountClient = grpcAccountClient;
    }

    public async Task<Result<GetUserDetailsResponse?>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var currentAccountId = request.CurrentAccountId;

        if (accountId == Guid.Empty)
        {
            return Result<GetUserDetailsResponse>.Failure(UserError.NullParameters, "Account Id is null");
        }

        var user = await _context.Users
            .Where(user => user.AccountId == accountId)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return Result<GetUserDetailsResponse?>.Failure(UserError.NotFound);
        }

        var grpcRequest = new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString()
        };

        var grpcResponse = await _grpcAccountClient.GetAccountDetailAsync(grpcRequest, cancellationToken: cancellationToken);

        if (grpcResponse == null)
        {
            return Result<GetUserDetailsResponse>.Failure(UserError.NotFound);
        }

        var result = _mapper.Map<GetUserDetailsResponse>(user);
        result.AccountPhoneNumber = grpcResponse.PhoneNumber;
        result.AccountEmail = grpcResponse.Email;
        result.CreatedAt = grpcResponse.CreatedAt.ToDateTime();
        result.UpdatedAt = grpcResponse.UpdatedAt.ToDateTime();

        if (currentAccountId != null && currentAccountId != Guid.Empty)
        {
            var follow = await _context.UserFollows.Where(f => f.FollowerId == currentAccountId && f.FollowingId == accountId).SingleOrDefaultAsync();
            result.IsFollowing = follow != null;
            result.IsCurrentUser = currentAccountId == accountId;
        }

        return Result<GetUserDetailsResponse?>.Success(result);
    }
}

