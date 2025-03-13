using Contract.Common;

namespace Contract.Interfaces;

public class PaginateParam
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public SortType? SortOrder { get; set; } = SortType.DESC;
    public string? SortBy { get; set; }
}

public enum SortType
{
    ASC,
    DESC
}

public interface IPaginateDataUtility<Type, ResponseMetadataType>
    where Type : class
    where ResponseMetadataType : class
{
    ReturnType Paginate<ReturnType>(IQueryable<Type> query, PaginateParam param)
        where ReturnType : BasePaginatedResponse<Type, ResponseMetadataType>, new();
    IQueryable<Type> PaginateQuery(IQueryable<Type> query, PaginateParam param);
}
