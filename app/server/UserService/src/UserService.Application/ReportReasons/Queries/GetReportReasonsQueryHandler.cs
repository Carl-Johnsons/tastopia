using Contract.Constants;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.ReportReasons.Queries;
public class GetReportReasonsQuery : IRequest<Result<List<ReportReasonResponse>?>>
{
    public string Language { get; set; } = null!;
}

public class GetReportReasonsQueryHandler : IRequestHandler<GetReportReasonsQuery, Result<List<ReportReasonResponse>?>>
{
    public Task<Result<List<ReportReasonResponse>?>> Handle(GetReportReasonsQuery request, CancellationToken cancellationToken)
    {
        var lang = request.Language;

        if (string.IsNullOrEmpty(lang))
        {
            return Task.FromResult(Result<List<ReportReasonResponse>?>.Failure(UserError.NullParameters));
        }
      
        var reasons = lang == LanguageValidation.En
            ? ReportReasonData.ReportUserReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.En }).ToList()
            : ReportReasonData.ReportUserReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.Vi }).ToList();
        if (reasons == null || reasons.Count == 0){
            return Task.FromResult(Result<List<ReportReasonResponse>?>.Failure(UserError.NotFound, "Not found report user reason"));
        }
        return Task.FromResult(Result<List<ReportReasonResponse>?>.Success(reasons));
    }
}
