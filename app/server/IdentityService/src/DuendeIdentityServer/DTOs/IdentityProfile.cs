using AutoMapper;
using Contract.DTOs.UserDTO;
using IdentityService.Application.Account.Commands;

namespace DuendeIdentityServer.DTOs;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<RegisterAccountDTO, RegisterAccountCommand>().ReverseMap();
        CreateMap<ApplicationAccount, ApplicationUserResponseDTO>().ReverseMap();
        CreateMap<VerifyAccountDTO, VerifyAccountCommand>().ReverseMap();
        CreateMap<ApplicationAccount, AccountDTO>().ReverseMap();
    }
}
