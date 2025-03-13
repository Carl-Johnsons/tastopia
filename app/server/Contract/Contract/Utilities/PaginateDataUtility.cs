using Contract.Common;
using Contract.Interfaces;
using System.Linq.Expressions;

namespace Contract.Utilities;

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
        if(param.SortBy != null && param.SortOrder != null)
        {
                var parameter = Expression.Parameter(typeof(Type), "x");
                var property = Expression.Property(parameter, param.SortBy);
                var keySelector = Expression.Lambda<Func<Type, object>>(Expression.Convert(property, typeof(object)), parameter);
                query = param.SortOrder == SortType.ASC ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

        }
        return query.Skip(param.Offset)
                    .Take(param.Limit);
    }
}
