﻿using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class PaginatedRecipeFeedsListResponse : BasePaginatedResponse<RecipeFeedResponse, AdvancePaginatedMetadata>;

public class PaginatedSearchRecipeListResponse : BasePaginatedResponse<SearchRecipesResponse, AdvancePaginatedMetadata>;

public class PaginatedTagListResponse : BasePaginatedResponse<TagResponse, AdvancePaginatedMetadata>;

public class PaginatedRecipeCommentListResponse : BasePaginatedResponse<RecipeCommentResponse, AdvancePaginatedMetadata>;

public class PaginatedAccountRecipeCommentListResponse : BasePaginatedResponse<AccountRecipeCommentResponse, AdvancePaginatedMetadata>;

public class PaginatedUserActivityListResponse : BasePaginatedResponse<UserActivityResponse, AdvancePaginatedMetadata>;

public class PaginatedAdminRecipeListResponse : BasePaginatedResponse<AdminRecipeResponse, NumberedPaginatedMetadata>;

public class PaginatedAdminReportRecipeListResponse : BasePaginatedResponse<AdminReportRecipeResponse, NumberedPaginatedMetadata>;

public class PaginatedAdminReportCommentListResponse : BasePaginatedResponse<AdminReportCommentResponse, NumberedPaginatedMetadata>;

public class PaginatedAdminTagListResponse : BasePaginatedResponse<AdminTagResponse, NumberedPaginatedMetadata>;