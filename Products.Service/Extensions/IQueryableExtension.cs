using System.Linq.Expressions;

namespace Products.Service.Extensions;

public static class IQueryableExtension
{
    public static IQueryable<T> CustomOrderBy<T>(this IQueryable<T> source, string orderBy,bool descending = true)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        MemberExpression property = Expression.Property(parameter, orderBy);
        Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

        
        if (descending)
            return source.OrderByDescending(lambda);
        

        return source.OrderBy(lambda);
    }
   
}