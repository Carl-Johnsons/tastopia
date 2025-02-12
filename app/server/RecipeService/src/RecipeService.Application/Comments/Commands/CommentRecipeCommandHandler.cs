using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.NotificationEvent;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Comments.Commands;
public class CommentRecipeCommand : IRequest<Result<RecipeCommentResponse?>>
{
    public Guid? RecipeId { get; init; }

    public Guid? AccountId { get; init; }
    public string? Content { get; init; }
}

public class CommentRecipeCommandHandler : IRequestHandler<CommentRecipeCommand, Result<RecipeCommentResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly ILogger<CommentRecipeCommandHandler> _logger;


    public CommentRecipeCommandHandler(IApplicationDbContext context,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        GrpcUser.GrpcUserClient grpcUserClient,
        IServiceBus serviceBus,
        ILogger<CommentRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
        _serviceBus = serviceBus;
        _logger = logger;
    }

    public async Task<Result<RecipeCommentResponse?>> Handle(CommentRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var recipeId = request.RecipeId;
            var accountId = request.AccountId;
            var content = request.Content;

            if (recipeId == null || accountId == null || string.IsNullOrEmpty(content))
            {
                return Result<RecipeCommentResponse?>.Failure(CommentError.AddCommentFail);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return Result<RecipeCommentResponse?>.Failure(CommentError.NotFound);
            }

            List<string> accountList = [accountId.ToString()!];

            var response = _grpcUserClient.GetSimpleUser(new GrpcGetSimpleUsersRequest
            {
                AccountId = { _mapper.Map<RepeatedField<string>>(accountList) }
            });

            var mapUsers = new Dictionary<Guid, SimpleUser>();
            foreach (var (key, value) in response.Users)
            {
                mapUsers[Guid.Parse(key)] = new SimpleUser
                {
                    AccountId = Guid.Parse(value.AccountId),
                    AvtUrl = value.AvtUrl,
                    DisplayName = value.DisplayName,
                };
            }

            if (response == null || mapUsers[accountId.Value] == null)
            {
                return Result<RecipeCommentResponse?>.Failure(CommentError.AddCommentFail);
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                AccountId = accountId.Value,
                Content = content,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var user = mapUsers[accountId.Value];
            var result = new RecipeCommentResponse
            {
                Id = comment.Id,
                AccountId = user.AccountId,
                AvatarUrl = user.AvtUrl,
                DisplayName = user.DisplayName,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                IsActive = comment.IsActive,
            };

            recipe.Comments.Add(comment);
            recipe.NumberOfComment += 1;
            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            // Publish notification to the author of the recipe
            if (recipe.AuthorId != accountId.Value)
            {
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
                    TemplateCode = NotificationTemplateCode.USER_COMMENT,
                    Channels = [NOTIFICATION_CHANNEL.DEFAULT],
                    JsonData = JsonConvert.SerializeObject(new
                    {
                        redirectUri = $"{CLIENT_URI.MOBILE.COMMUNITY}/{recipeId}"
                    }),
                    ImageUrl = user.AvtUrl
                });
            }
            return Result<RecipeCommentResponse?>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<RecipeCommentResponse?>.Failure(CommentError.AddCommentFail, ex.Message);
        }
    }
}
