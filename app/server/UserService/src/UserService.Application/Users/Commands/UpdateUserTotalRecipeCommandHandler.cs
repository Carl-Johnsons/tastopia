using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Errors;
namespace UserService.Application.Users.Commands;
public record UpdateUserTotalRecipeCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; init; }
    public int Delta {  get; init; }
}
public class UpdateUserTotalRecipeCommandHandler : IRequestHandler<UpdateUserTotalRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserTotalRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserTotalRecipeCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.AccountId == request.AccountId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserError.NotFound);
        }

        user.TotalRecipe = (user.TotalRecipe ?? 0) + request.Delta;
        if (user.TotalRecipe < 0)
        {
            user.TotalRecipe = 0;
        }
        _context.Users.Update(user);
        await _unitOfWork.SaveChangeAsync();
        return Result.Success();
    }
}
