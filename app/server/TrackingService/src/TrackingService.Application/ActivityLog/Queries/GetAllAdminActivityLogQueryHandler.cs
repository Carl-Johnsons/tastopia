using AutoMapper;
using Contract.DTOs;
using Google.Protobuf.Collections;
using TrackingService.Domain.Responses;
using UserProto;

namespace TrackingService.Application.ActivityLog.Queries;

public record GetAllAdminActivityLogQuery : IRequest<Result<PaginatedAdminActivityLogListResponse>>
{
    public PaginatedDTO DTO { get; init; } = null!;
}


public class GetAllAdminActivityLogQueryHandler : IRequestHandler<GetAllAdminActivityLogQuery, Result<PaginatedAdminActivityLogListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    public GetAllAdminActivityLogQueryHandler(IApplicationDbContext context,
                                           IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> paginateDataUtility,
                                           IMapper mapper,
                                           GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<PaginatedAdminActivityLogListResponse>> Handle(GetAllAdminActivityLogQuery request,
                                                                      CancellationToken cancellationToken)
    {
        var keyword = request.DTO.Keyword;
        var limit = request.DTO.Limit ?? ENTITY_LIMIT.ADMIN_ACTIVITY_LOG;
        var offset = (request.DTO.Skip * limit ?? 0);

        var query = _context.AdminActivityLogs;

        var accountIds = query.Select(aal => aal.AccountId).ToList();
        var repeatedField = _mapper.Map<RepeatedField<string>>(accountIds);

        if (accountIds == null || accountIds.Count == 0)
        {
            return Result<PaginatedAdminActivityLogListResponse>.Success(new PaginatedAdminActivityLogListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (request.DTO.Skip ?? 0) + 1,
                    TotalPage = 0,
                    TotalRow = 0
                }
            });
        }

        var mapGrpcUser = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField }
        }, cancellationToken: cancellationToken);

        var formatQuery = query
            .AsEnumerable() // Load the memory first
            .Select(aal => new AdminActivityLogResponse
            {
                AccountId = aal.AccountId,
                AccountUsername = mapGrpcUser.Users[aal.AccountId.ToString()].AccountUsername,
                ActivityType = aal.ActivityType,
                EntityId = aal.EntityId,
                EntityType = aal.EntityType,
                SecondaryEntityId = aal.SecondaryEntityId,
                SecondaryEntityType = aal.SecondaryEntityType,
                CreatedAt = aal.CreatedAt,
                UpdatedAt = aal.UpdatedAt
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            formatQuery = formatQuery.Where(aal => aal.AccountUsername!.ToLower().Contains(keyword)
                                                || aal.EntityType.ToString().ToLower().Contains(keyword));
        }

        var totalRow = query.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        var paginatedQuery = _paginateDataUtility.PaginateQuery(formatQuery, new PaginateParam
        {
            Limit = limit,
            Offset = offset,
            SortBy = request.DTO.SortBy ?? "CreatedAt",
            SortOrder = request.DTO.SortOrder ?? SortType.DESC
        });

        if (paginatedQuery.Count() == 0)
        {
            return Result<PaginatedAdminActivityLogListResponse>.Success(new PaginatedAdminActivityLogListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (request.DTO.Skip ?? 0) + 1,
                    TotalPage = totalPage,
                    TotalRow = totalRow
                }
            });
        }

        var list = paginatedQuery.ToList();

        var paginatedResponse = new PaginatedAdminActivityLogListResponse
        {
            PaginatedData = list,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (request.DTO.Skip ?? 0) + 1,
                TotalPage = totalPage,
                TotalRow = totalRow
            }
        };

        return Result<PaginatedAdminActivityLogListResponse>.Success(paginatedResponse);
    }
}
