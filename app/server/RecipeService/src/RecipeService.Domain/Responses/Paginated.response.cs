using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class PaginatedRecipeFeedsListResponse : BasePaginatedResponse<RecipeFeedResponse, AdvancePaginatedMetadata>;

public class PaginatedSearchRecipeListResponse : BasePaginatedResponse<SearchRecipesResponse, AdvancePaginatedMetadata>;

public class PaginatedTagListResponse : BasePaginatedResponse<Tag, AdvancePaginatedMetadata>;



