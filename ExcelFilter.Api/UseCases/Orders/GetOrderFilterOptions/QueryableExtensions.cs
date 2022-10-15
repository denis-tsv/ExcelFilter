using System.Linq.Expressions;
using System.Reflection;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;

public static class QueryableExtensions
{
    private static readonly ConstructorInfo ConstructorInfo = typeof(FilterOption).GetConstructors().First();
    private static readonly MemberInfo KeyPropertyInfo = typeof(FilterOption).GetProperty(nameof(FilterOption.Key))!;
    private static readonly MemberInfo LabelPropertyInfo = typeof(FilterOption).GetProperty(nameof(FilterOption.Label))!;

    public static IQueryable<FilterOption> ToFilterOptions<TEntity>(
        this IQueryable<TEntity> q,
        Expression<Func<TEntity, object>> keyExpression,
        Expression<Func<TEntity, string>> labelExpression)
    {
        var newExpression = Expression.New(ConstructorInfo);

        var e2Rebind = ParameterRebinder.ReplaceParameters(keyExpression.Parameters.First(), labelExpression.Parameters.First(), keyExpression.Body);
        var e1ExpressionBind = Expression.Bind(LabelPropertyInfo, labelExpression.Body);
        var e2ExpressionBind = Expression.Bind(KeyPropertyInfo, e2Rebind);

        var result = Expression.MemberInit(newExpression, e1ExpressionBind, e2ExpressionBind);
        
        var lambda = Expression.Lambda<Func<TEntity, FilterOption>>(result, labelExpression.Parameters);

        return q.Select(lambda).Distinct().OrderBy(x => x.Key);
    }

    private class ParameterRebinder : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly ParameterExpression _to;

        private ParameterRebinder(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }

        public static Expression ReplaceParameters(ParameterExpression from, ParameterExpression to, Expression exp)
        {
            return new ParameterRebinder(from, to).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (p == _from)
            {
                p = _to;
            }

            return base.VisitParameter(p);
        }
    }
}