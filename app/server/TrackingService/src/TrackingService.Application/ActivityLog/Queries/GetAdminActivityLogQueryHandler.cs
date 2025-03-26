using AutoMapper;
using Contract.Constants;
using Contract.DTOs;
using Google.Protobuf.Collections;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using RecipeProto;
using TrackingService.Domain.Responses;

namespace TrackingService.Application.ActivityLog.Queries;

public record GetAdminActivityLogQuery : IRequest<Result<PaginatedAdminActivityLogListResponse>>
{
    public PaginatedDTO DTO { get; init; } = null!;
    public Guid AccountId { get; init; }
}


public class GetAdminActivityLogQueryHandler : IRequestHandler<GetAdminActivityLogQuery, Result<PaginatedAdminActivityLogListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    public GetAdminActivityLogQueryHandler(IApplicationDbContext context,
                                           IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> paginateDataUtility,
                                           IMapper mapper,
                                           GrpcRecipe.GrpcRecipeClient grpcRecipeClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcRecipeClient = grpcRecipeClient;
    }

    public async Task<Result<PaginatedAdminActivityLogListResponse>> Handle(GetAdminActivityLogQuery request,
                                                                      CancellationToken cancellationToken)
    {
        var limit = request.DTO.Limit ?? ENTITY_LIMIT.ADMIN_ACTIVITY_LOG;
        var offset = (request.DTO.Skip * limit ?? 0);

        var query = _context.AdminActivityLogs
            .Where(aal => aal.AccountId == request.AccountId)
            .Select(aal => new AdminActivityLogResponse
            {
                AccountId = aal.AccountId,
                ActivityType = aal.ActivityType,
                EntityId = aal.EntityId,
                EntityType = aal.EntityType,
                SecondaryEntityId = aal.SecondaryEntityId,
                SecondaryEntityType = aal.SecondaryEntityType,
                CreatedAt = aal.CreatedAt,
                UpdatedAt = aal.UpdatedAt
            });

        var totalRow = query.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        var paginatedQuery = _paginateDataUtility.PaginateQuery(query, new PaginateParam
        {
            Limit = limit,
            Offset = offset,
            SortBy = "CreatedAt",
            SortOrder = SortType.DESC
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
        Console.WriteLine("Count is " + paginatedQuery.Count());

        var recipeIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.RECIPE || (aal.SecondaryEntityId != null && aal.SecondaryEntityType == ActivityEntityType.RECIPE))
            .Select(aal => (Guid)(aal.EntityType == ActivityEntityType.RECIPE ? aal.EntityId : aal.SecondaryEntityId!))
            .ToList();

        GrpcMapSimpleRecipes? grpcRecipeMap = null;

        if (recipeIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(recipeIdList);

            grpcRecipeMap = await _grpcRecipeClient.GetSimpleRecipesAsync(new GrpcGetSimpleRecipeRequest
            {
                AccountId = request.AccountId.ToString(),
                RecipeIds = { repeatedField }
            });
        }


        var list = paginatedQuery.ToList()
                                 .Select(aal =>
                                 {
                                     if (aal.EntityType == ActivityEntityType.RECIPE && grpcRecipeMap != null)
                                     {
                                         var grpcSimpleRecipe = grpcRecipeMap.Recipes[aal.EntityId.ToString()];
                                         
                                         var mapEntity = new RecipeAdminActivityLogResponse {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };
                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             RecipeTitle = grpcSimpleRecipe.Title,
                                             RecipeAuthorId = Guid.Parse(grpcSimpleRecipe.AuthorId),
                                             RecipeAuthorDisplayName = grpcSimpleRecipe.AuthorDisplayName,
                                             RecipeImageURL = grpcSimpleRecipe.RecipeImgUrl,
                                             RecipeCreatedAt = grpcSimpleRecipe.CreatedAt.ToDateTime(),
                                             RecipeUpdatedAt = grpcSimpleRecipe.UpdatedAt.ToDateTime(),
                                             RecipeVoteDiff = grpcSimpleRecipe.VoteDiff
                                         };

                                         mapEntity.Recipe = recipeLogResponse;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.COMMENT && grpcRecipeMap != null)
                                     {
                                         var grpcSimpleRecipe = grpcRecipeMap.Recipes[aal.SecondaryEntityId.ToString()];

                                         var mapEntity = new CommentAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             RecipeTitle = grpcSimpleRecipe.Title,
                                             RecipeAuthorId = Guid.Parse(grpcSimpleRecipe.AuthorId),
                                             RecipeAuthorDisplayName = grpcSimpleRecipe.AuthorDisplayName,
                                             RecipeImageURL = grpcSimpleRecipe.RecipeImgUrl,
                                             RecipeCreatedAt = grpcSimpleRecipe.CreatedAt.ToDateTime(),
                                             RecipeUpdatedAt = grpcSimpleRecipe.UpdatedAt.ToDateTime(),
                                             RecipeVoteDiff = grpcSimpleRecipe.VoteDiff
                                         };

                                         var commentLogResponse = new CommentLogResponse { 
                                            
                                         };

                                         mapEntity.Recipe = recipeLogResponse;

                                         return mapEntity;
                                     }

                                     return aal;
                                 });



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

        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(paginatedResponse, Formatting.Indented));
        return Result<PaginatedAdminActivityLogListResponse>.Success(paginatedResponse);
    }
}
