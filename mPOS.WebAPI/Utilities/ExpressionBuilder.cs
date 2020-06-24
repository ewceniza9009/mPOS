using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace mPOS.WebAPI.Utilities
{
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            switch (filters.Count)
            {
                case 1:
                    exp = GetExpression<T>(param, filters[0]);
                    break;
                case 2:
                    exp = GetExpression<T>(param, filters[0], filters[1]);
                    break;
                default:
                {
                    while (filters.Count > 0)
                    {
                        var f1 = filters[0];
                        var f2 = filters[1];

                        exp = exp == null
                            ? GetExpression<T>(param, filters[0], filters[1])
                            : Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                        filters.Remove(f1);
                        filters.Remove(f2);

                        if (filters.Count != 1) continue;
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }

                    break;
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp ?? throw new InvalidOperationException(), param);
        }

        private static Expression GetExpression<T>(Expression param, Filter filter)
        {
            var member = Expression.Property(param, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case POCO.Operation.Equals:
                    return Expression.Equal(member, constant);

                case POCO.Operation.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case POCO.Operation.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case POCO.Operation.LessThan:
                    return Expression.LessThan(member, constant);

                case POCO.Operation.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case POCO.Operation.Contains:
                    return new CaseInsensitiveExpressionVisitor().Visit(Expression.Call(member, containsMethod, constant));

                case POCO.Operation.StartsWith:
                    return new CaseInsensitiveExpressionVisitor().Visit(Expression.Call(member, startsWithMethod, constant));

                case POCO.Operation.EndsWith:
                    return new CaseInsensitiveExpressionVisitor().Visit(Expression.Call(member, endsWithMethod, constant));
                default:
                    return null;
            }
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filter filter1, Filter filter2)
        {
            var bin1 = GetExpression<T>(param, filter1);
            var bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
    public class Filter
    {
        public string PropertyName { get; set; }
        public POCO.Operation Operation { get; set; }
        public object Value { get; set; }
    }

    public class CaseInsensitiveExpressionVisitor : ExpressionVisitor
    {
        private Boolean insideContains = false;
        protected override Expression VisitMember(MemberExpression node)
        {
            if (insideContains)
            {
                if (node.Type == typeof(String))
                {
                    var methodInfo = typeof(String).GetMethod("ToLower", new Type[] { });
                    var expression = Expression.Call(node, methodInfo);
                    return expression;
                }
            }
            return base.VisitMember(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Contains")
            {
                if (insideContains) throw new NotSupportedException();
                insideContains = true;
                var result = base.VisitMethodCall(node);
                insideContains = false;
                return result;
            }
            return base.VisitMethodCall(node);
        }
    }

    /*Usage
    static void Main(string[] args)
    {
        Expression<Func<Test, bool>> expr = (t) => t.Name.Contains("a");
        var expr1 = (Expression<Func<Test, bool>>)new CaseInsensitiveExpressionVisitor().Visit(expr);
        var test = new[] { new Test { Name = "A" } };
        var length = test.Where(expr1.Compile()).ToArray().Length;
        Debug.Assert(length == 1);
        Debug.Assert(test.Where(expr.Compile()).ToArray().Length == 0);
    }
    */
}