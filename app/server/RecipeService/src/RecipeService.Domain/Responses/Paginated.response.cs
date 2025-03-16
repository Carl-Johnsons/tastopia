using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class PaginatedRecipeFeedsListResponse : BasePaginatedResponse<RecipeFeedResponse, AdvancePaginatedMetadata>;

public class PaginatedSearchRecipeListResponse : BasePaginatedResponse<SearchRecipesResponse, AdvancePaginatedMetadata>;

public class PaginatedTagListResponse : BasePaginatedResponse<Tag, AdvancePaginatedMetadata>;

public class PaginatedRecipeCommentListResponse : BasePaginatedResponse<RecipeCommentResponse, AdvancePaginatedMetadata>;

public class PaginatedAccountRecipeCommentListResponse : BasePaginatedResponse<AccountRecipeCommentResponse, AdvancePaginatedMetadata>;

public class PaginatedUserActivityListResponse : BasePaginatedResponse<UserActivityResponse, AdvancePaginatedMetadata>;

public class PaginatedAdminRecipeListResponse : BasePaginatedResponse<AdminRecipeResponse, NumberedPaginatedMetadata>;

public class PaginatedAdminReportRecipeListResponse : BasePaginatedResponse<AdminReportRecipeResponse, NumberedPaginatedMetadata>;