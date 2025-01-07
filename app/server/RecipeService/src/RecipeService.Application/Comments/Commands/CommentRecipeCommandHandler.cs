using AutoMapper;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;

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
    private readonly IServiceBus _serviceBus;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentRecipeCommandHandler(IApplicationDbContext context, IServiceBus serviceBus, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _serviceBus = serviceBus;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

            var requestClient = _serviceBus.CreateRequestClient<GetSimpleUsersEvent>();

            var response = await requestClient.GetResponse<GetSimpleUsersDTO>(new GetSimpleUsersEvent
            {
                AccountIds = new HashSet<Guid> { accountId.Value }
            });

            if (response == null || response.Message.Users[accountId.Value] == null)
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

            var user = response.Message.Users[accountId.Value];
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
                RecipeId = recipe.Id,
            };

            recipe!.Comments.Add(comment);
            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result<RecipeCommentResponse?>.Success(result);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(ex));
            return Result<RecipeCommentResponse?>.Failure(CommentError.AddCommentFail);
        }
    }
}
