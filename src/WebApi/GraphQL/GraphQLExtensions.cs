using GraphQL;
using System.Linq.Expressions;

namespace WebApi.GraphQL;

public static class GraphQLExtensions
{
    public static IQueryable<T> SelectProjectTo<T>(this IQueryable<T> query, List<string> fields)
    {
        if (fields == null || !fields.Any())
            return query;

        // input parameter "p" stands for predicate
        var expressionParameter = Expression.Parameter(typeof(T), "p");

        // new statement "new Data()"
        var expressionNew = Expression.New(typeof(T));

        // create initializers
        var bindings = fields.Select(p => p.Trim())
            .Select(p =>
            {

                // property "Field1"
                var mi = typeof(T).GetProperty(p.ToPascalCase());

                // original value "p.Field1"
                var xOriginal = Expression.Property(expressionParameter, mi);

                // set value "Field1 = p.Field1"
                return Expression.Bind(mi, xOriginal);
            }
        );

        // initialization "new Data { Field1 = p.Field1, Field2 = p.Field2 }"
        var memberInitExpression = Expression.MemberInit(expressionNew, bindings);

        // expression "p => new Data { Field1 = p.Field1, Field2 = p.Field2 }"
        Expression<Func<T, T>> lambdaExpression = Expression.Lambda<Func<T, T>>(memberInitExpression, expressionParameter);

        return query.Select(lambdaExpression);
    }

    public static Expression<Func<T, T>> SelectProjectTo<T>(List<string> fields)
    {
        if (fields == null || !fields.Any())
            throw new ArgumentNullException("fields prarameter should contains entity field names.");

        // input parameter "p" stands for predicate
        var expressionParameter = Expression.Parameter(typeof(T), "p");

        // new statement "new Data()"
        var expressionNew = Expression.New(typeof(T));

        // create initializers
        var bindings = fields.Select(p => p.Trim())
            .Select(p =>
            {

                // property "Field1"
                var mi = typeof(T).GetProperty(p.ToPascalCase());

                // original value "p.Field1"
                var xOriginal = Expression.Property(expressionParameter, mi);

                // set value "Field1 = p.Field1"
                return Expression.Bind(mi, xOriginal);
            }
        );

        // initialization "new Data { Field1 = p.Field1, Field2 = p.Field2 }"
        var memberInitExpression = Expression.MemberInit(expressionNew, bindings);

        // expression "p => new Data { Field1 = p.Field1, Field2 = p.Field2 }"
        Expression<Func<T, T>> lambdaExpression = Expression.Lambda<Func<T, T>>(memberInitExpression, expressionParameter);

        return lambdaExpression;
    }
}

