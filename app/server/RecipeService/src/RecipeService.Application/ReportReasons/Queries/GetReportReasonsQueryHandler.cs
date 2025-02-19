using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using Contract.Constants;

namespace RecipeService.Application.ReportReasons.Queries;
public class GetReportReasonsQuery : IRequest<Result<List<ReportReasonResponse>?>>
{
    public string ReportType { get; set; } = null!;
    public string Language { get; set; } = null!;
}

public class GetReportReasonsQueryHandler : IRequestHandler<GetReportReasonsQuery, Result<List<ReportReasonResponse>?>>
{
    public async Task<Result<List<ReportReasonResponse>?>> Handle(GetReportReasonsQuery request, CancellationToken cancellationToken)
    {
        var lang = request.Language;
        var type = request.ReportType;

        if (string.IsNullOrEmpty(lang) || string.IsNullOrEmpty(type))
        {
            return Result<List<ReportReasonResponse>?>.Failure(RecipeError.NullParameter);
        }

        if (type == "Recipe")
        {
            var reasons = lang == LanguageValidation.En
                ? ReportReasonData.RecipeReportReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.En }).ToList()
                : ReportReasonData.RecipeReportReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.Vi }).ToList();

            if(reasons == null || reasons.Count == 0)
            {
                return Result<List<ReportReasonResponse>?>.Failure(RecipeError.NotFound, "Not found report recipe reason");

            }
            return Result<List<ReportReasonResponse>?>.Success(reasons);
        }

        if (type == "Comment")
        {
            var reasons = lang == LanguageValidation.En
                ? ReportReasonData.CommentReportReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.En }).ToList()
                : ReportReasonData.CommentReportReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.Vi }).ToList();

            if (reasons == null || reasons.Count == 0)
            {
                return Result<List<ReportReasonResponse>?>.Failure(RecipeError.NotFound, "Not found report comment reason");

            }
            return Result<List<ReportReasonResponse>?>.Success(reasons);
        }

        return Result<List<ReportReasonResponse>?>.Failure(RecipeError.NotFound);
    }
}
