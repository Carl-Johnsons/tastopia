using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Reports.Commands;

public record MarkAllRecipeReportCommand : IRequest<Result>
{
    public Guid RecipeId { get; set; }
    public Guid CurrentAccountId { get; set; }
    public bool IsReopened { get; set; }
}

public class MarkAllRecipeReportCommandHandler : IRequestHandler<MarkAllRecipeReportCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    public MarkAllRecipeReportCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(MarkAllRecipeReportCommand request,
                               CancellationToken cancellationToken)
    {
        if(request.RecipeId == Guid.Empty)
        {
            return Result.Failure(ReportError.NullParameter, "RecipeId not found");
        }

        var reports = await _context.UserReportRecipes.Where(rp => rp.EntityId == request.RecipeId && rp.Status == (request.IsReopened ? ReportStatus.Done : ReportStatus.Pending)).ToListAsync();
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(reports, Formatting.Indented));
        if(reports == null || reports.Count == 0)
        {
            return Result.Failure(ReportError.NotFound, "Not found recipe reports");
        }
        if (reports != null && reports.Count != 0)
        {
            foreach (var rp in reports)
            {
                rp.Status = request.IsReopened ? ReportStatus.Pending : ReportStatus.Done;
            }
            _context.UserReportRecipes.UpdateRange(reports);
        }

        await _unitOfWork.SaveChangeAsync();
        foreach (var rs in reports!)
        {
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = request.IsReopened ? ActivityType.REOPEN : ActivityType.MARK_COMPLETE,
                EntityId = rs.Id,
                EntityType = ActivityEntityType.REPORT_RECIPE
            });

        }
        return Result.Success();
    }

}
