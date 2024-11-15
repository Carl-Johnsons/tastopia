namespace NotificationService.Domain.Interfaces;

public class PaginateParam
{
    public int Offset { get; set; }
    public int Limit { get; set; }
}

public interface IPaginateDataUtility<Type, ResponseMetadataType>
    where Type : class
    where ResponseMetadataType : class
{
    ReturnType Paginate<ReturnType>(IQueryable<Type> query, PaginateParam param)
        where ReturnType : BasePaginatedResponse<Type, ResponseMetadataType>, new();
    IQueryable<Type> PaginateQuery(IQueryable<Type> query, PaginateParam param);
}
