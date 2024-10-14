using PostService.Domain.Constants;

namespace PostService.Infrastructure.Persistence.Mockup.Data;

public class UserContentRestrictionsType : BaseEntity
{
    public string Code { get; set; } = null!;
}

public static class UserContentRestrictionsTypeMockup
{
    public static UserContentRestrictionsType[] Data { get; set; } = [
        new UserContentRestrictionsType  {
            Code = USER_CONTENT_RESTRICTIONS_TYPE.MUTE_COMMENT,
        },
        new UserContentRestrictionsType  {
            Code = USER_CONTENT_RESTRICTIONS_TYPE.BAN_POST,
        },
        ];
}
