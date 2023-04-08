using System.Linq.Expressions;
using Products.Domain.Filters;

namespace Products.Service.Extensions;

public static class IOrderedQueryableExtension
{
    public static IQueryable<T> CustomOrderBy<T>(this IQueryable<T> source,
        List<OrderByParameter> orderParams)
    {
        if (orderParams.Count == 0)
            return source;

        IOrderedQueryable<T> orderedQuery = orderParams.First().IsDescending
            ? source.OrderByDescending(GetLmabda<T>(orderParams.First()))
            : source.OrderBy(GetLmabda<T>(orderParams.First()));
        
        orderParams.RemoveAt(index: 0);
        
        foreach (var param in orderParams)
        {
            if (param.IsDescending)
                orderedQuery = orderedQuery.ThenByDescending(GetLmabda<T>(param));

            if (!param.IsDescending)
                orderedQuery = orderedQuery.ThenBy(GetLmabda<T>(param));
        }

        source = orderedQuery;
        return source;
    }

    public static Expression<Func<TEntity, object>> GetLmabda<TEntity>(OrderByParameter param)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression property = Expression.Property(parameter, param.Property);
        return Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);
    }
}