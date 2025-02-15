using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.ReportReasons.Queries;
public class GetReportReasonsQuery : IRequest<Result<List<ReportReasonResponse>?>>
{
    public string Language { get; set; } = null!;
}

public class GetReportReasonsQueryHandler : IRequestHandler<GetReportReasonsQuery, Result<List<ReportReasonResponse>?>>
{
    public async Task<Result<List<ReportReasonResponse>?>> Handle(GetReportReasonsQuery request, CancellationToken cancellationToken)
    {
        var lang = request.Language;

        if (string.IsNullOrEmpty(lang))
        {
            return Result<List<ReportReasonResponse>?>.Failure(UserError.NullParameters);
        }
      
        var reasons = lang == "English"
            ? ReportReasonData.ReportUserReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.Eng }).ToList()
            : ReportReasonData.ReportUserReasons.Select(r => new ReportReasonResponse { Code = r.Code, Content = r.Vn }).ToList();
        if (reasons == null || reasons.Count == 0){
            return Result<List<ReportReasonResponse>?>.Failure(UserError.NotFound, "Not found report user reason");
        }
        return Result<List<ReportReasonResponse>?>.Success(reasons);
    }
}
