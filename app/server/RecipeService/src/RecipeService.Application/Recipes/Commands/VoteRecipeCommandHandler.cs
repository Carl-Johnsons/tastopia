using Contract.Constants;
using Contract.Event.NotificationEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes.Commands;

public class VoteRecipeCommand : IRequest<Result<VoteResponse?>>
{
    [Required]
    public bool? IsUpvote { get; init; } = null!;
    [Required]
    public Guid? AccountId { get; init; } = null!;
    [Required]
    public Guid? RecipeId { get; init; } = null!;
}

public class VoteRecipeCommandHandler : IRequestHandler<VoteRecipeCommand, Result<VoteResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<VoteRecipeCommandHandler> _logger;
    private readonly IServiceBus _serviceBus;

    public VoteRecipeCommandHandler(IApplicationDbContext context,
                                    IUnitOfWork unitOfWork,
                                    ILogger<VoteRecipeCommandHandler> logger,
                                    IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public async Task<Result<VoteResponse?>> Handle(VoteRecipeCommand request,
                                                    CancellationToken cancellationToken)
    {
        try
        {
            var accountId = request.AccountId;
            var recipeId = request.RecipeId;
            var isUpvote = request.IsUpvote;

            if (accountId == null || recipeId == null || isUpvote == null)
            {
                return Result<VoteResponse?>.Failure(RecipeError.NotFound);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId && r.IsActive == true).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return Result<VoteResponse?>.Failure(RecipeError.NotFound);
            }

            var recipeVote = recipe.RecipeVotes.Where(rv => rv.AccountId == accountId).FirstOrDefault();
            var vote = (bool)isUpvote ? Vote.Upvote : Vote.Downvote;
            if (recipeVote == null)
            {
                recipeVote = new RecipeVote
                {
                    AccountId = accountId.Value,
                    IsUpvote = isUpvote.Value,
                };
                var delta = isUpvote.Value ? 1 : -1;
                recipe.VoteDiff += delta;
                recipe.RecipeVotes.Add(recipeVote);
                _context.Recipes.Update(recipe);
            }
            else
            {
                var delta = recipeVote.IsUpvote ? isUpvote.Value ? -1 : -2 : isUpvote.Value ? 2 : 1;
                if (recipeVote.IsUpvote == isUpvote.Value)
                {
                    vote = Vote.None;
                    recipe.RecipeVotes.Remove(recipeVote);

                }
                else
                {
                    recipeVote.IsUpvote = isUpvote.Value;
                }
                recipe.VoteDiff += delta;
                _context.Recipes.Update(recipe);
            }
            await _unitOfWork.SaveChangeAsync();

            // Notify other user
            if (vote != Vote.None && accountId.Value != recipe.AuthorId)
            {
                var templateCode = vote == Vote.Upvote ? NotificationTemplateCode.USER_UPVOTE : NotificationTemplateCode.USER_DOWNVOTE;

                await _serviceBus.Publish(new NotifyUserEvent
                {
                    PrimaryActors = [
                        new ActorDTO
                        {
                            ActorId = accountId.Value,
                            Type = EntityType.USER
                        }],
                    SecondaryActors = [
                        new ActorDTO
                        {
                            ActorId = recipe.AuthorId,
                            Type = EntityType.USER
                        }],
                    TemplateCode = templateCode,
                    Channels = [NOTIFICATION_CHANNEL.DEFAULT],
                    JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        redirectUri = $"{CLIENT_URI.MOBILE.COMMUNITY}/{recipeId}"
                    }),
                    ImageUrl = recipe.ImageUrl
                });

            }
            return Result<VoteResponse?>.Success(new VoteResponse
            {
                Vote = vote,
                AccountId = accountId.Value,
                RecipeId = recipe.Id,
            });

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
