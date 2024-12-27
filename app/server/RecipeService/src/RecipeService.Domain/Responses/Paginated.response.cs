using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class PaginatedRecipeFeedsListResponse : BasePaginatedResponse<RecipeFeedResponse, AdvancePaginatedMetadata>;

public class PaginatedSearchRecipeListResponse : BasePaginatedResponse<SearchRecipesResponse, CommonPaginatedMetadata>;

public class PaginatedTagListResponse : BasePaginatedResponse<Tag, CommonPaginatedMetadata>;



