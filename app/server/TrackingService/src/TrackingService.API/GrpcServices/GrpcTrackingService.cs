using Grpc.Core;
using TrackingProto;
using TrackingService.Application.UserViewRecipeDetails.Commands;

namespace TrackingService.API.GrpcServices;

public class GrpcTrackingService : GrpcTracking.GrpcTrackingBase
{
    private readonly ISender _sender;

    public GrpcTrackingService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<GrpcEmpty> GrpcUserViewRecipeDetail(GrpcUserViewRecipeDetailRequest request, ServerCallContext context)
    {
        var accountId = Guid.Parse(request.AccountId);
        var recipeId = Guid.Parse(request.RecipeId);
        var viewTime = request.ViewTime.ToDateTime();

        await _sender.Send(new CreateUserVewRecipeDetailCommand
        {
            AccountId = accountId,
            RecipeId = recipeId,
            ViewTime = viewTime,
        });
        return new GrpcEmpty();
    }
}
