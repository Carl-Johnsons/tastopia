namespace UserService.Domain.Responses;


public class PaginatedSimpleUserListResponse : BasePaginatedResponse<SimpleUserResponse, AdvancePaginatedMetadata>;
public class PaginatedAdminGetUserListResponse : BasePaginatedResponse<AdminGetUserResponse, NumberedPaginatedMetadata>;




