namespace PostService.Infrastructure.Utilities;

public class PaginateDataUtility<Type, ResponseMetadataType> : IPaginateDataUtility<Type, ResponseMetadataType>
    where Type : class
    where ResponseMetadataType : class
{
    public ReturnType Paginate<ReturnType>(IQueryable<Type> query, PaginateParam param)
        where ReturnType : BasePaginatedResponse<Type, ResponseMetadataType>, new()
    {
        IEnumerable<Type> paginatedData = [.. PaginateQuery(query, param)];

        return new ReturnType
        {
            PaginatedData = paginatedData
        };
    }

    public IQueryable<Type> PaginateQuery(IQueryable<Type> query, PaginateParam param)
    {
        return query.Skip(param.Offset)
                    .Take(param.Limit);
    }
}
