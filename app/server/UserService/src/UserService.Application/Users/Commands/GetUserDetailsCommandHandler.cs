using AutoMapper;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Commands;

public record GetUserDetailsCommand : IRequest<Result<GetUserDetailsResponse?>>
{
    [Required]
    public Guid? AccountId { get; init; }
}
public class GetUserDetailsCommandHandler : IRequestHandler<GetUserDetailsCommand, Result<GetUserDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IServiceBus _serviceBus;

    public GetUserDetailsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper, IServiceBus serviceBus)
    {
        _context = context;
        _mapper = mapper;
        _serviceBus = serviceBus;
    }

    public async Task<Result<GetUserDetailsResponse?>> Handle(GetUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;

        if(accountId == null)
        {
            return Result<GetUserDetailsResponse>.Failure(UserError.NotFound);
        }

        var user = await _context.Users
            .Where(user => user.AccountId == accountId)
            .FirstOrDefaultAsync();
        if (user == null) { 
            return Result<GetUserDetailsResponse?>.Failure(UserError.NotFound);
        }

        //var requestClient = _serviceBus.CreateRequestClient<GetUserDetailsEvent>();

        //var response = await requestClient.GetResponse<AccountDTO>(new GetUserDetailsEvent
        //{
        //    AccountId = accountId.Value,
        //});

        //if (response == null || response.Message == null)
        //{
        //    return Result<GetUserDetailsResponse>.Failure(UserError.NotFound);
        //}

        var result = _mapper.Map<GetUserDetailsResponse>(user);
        //result.AccountPhoneNumber = response.Message.PhoneNumber;
        //result.AccountEmail = response.Message.Email;

        return Result<GetUserDetailsResponse?>.Success(result);
    }
}

