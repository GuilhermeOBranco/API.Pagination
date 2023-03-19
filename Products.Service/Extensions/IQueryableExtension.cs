using System.Linq.Expressions;
using Products.Domain.Filters;

namespace Products.Service.Extensions;

public static class IOrderedQueryableExtension
{
    public static IOrderedQueryable<T> CustomOrderBy<T>(this IOrderedQueryable<T>source, List<OrderByParameter> orderParams)
    {
        bool isFirstOrder = true;
        
        foreach (var param in orderParams)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, param.Property);
            Expression<Func<T, object>> lambda = 
                Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (isFirstOrder && param.IsDescending)
            {
                source = source.OrderByDescending(lambda);
                isFirstOrder = false;
            }
            
            if (isFirstOrder && !param.IsDescending)
            {
                source = source.OrderBy(lambda);
                isFirstOrder = false;
            }
            
            if(param.IsDescending && !isFirstOrder)
                source = source.ThenByDescending(lambda);
            
            if(!param.IsDescending && !isFirstOrder)
                source = source.ThenBy(lambda);
            
        }

        return source;
    }
   
}