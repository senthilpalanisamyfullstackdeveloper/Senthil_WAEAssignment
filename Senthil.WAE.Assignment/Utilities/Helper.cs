namespace Senthil.WAE.Assignment.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Helper
    {
        public enum Operators
        {
            Equal = 13,
            GreaterThan = 14,
            GreaterThanOrEqualsTo = 15,
            LessThan = 20,
            LessThanOrEqualsTo = 21,
            NotEqual = 35
        }
    }
    
    public class Filter
    {
        public string PropertyName { get; set; }
        public Helper.Operators Operation { get; set; }
        public object Value { get; set; }
    }

    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var filter1 = filters[0];
                    var filter2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(filter1);
                    filters.Remove(filter2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            ConstantExpression constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case Helper.Operators.Equal:
                    return Expression.Equal(member, constant);

                case Helper.Operators.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Helper.Operators.GreaterThanOrEqualsTo:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Helper.Operators.LessThan:
                    return Expression.LessThan(member, constant);

                case Helper.Operators.LessThanOrEqualsTo:
                    return Expression.LessThanOrEqual(member, constant);

                case Helper.Operators.NotEqual:
                    return Expression.NotEqual(member, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression binary1 = GetExpression<T>(param, filter1);
            Expression binary2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(binary1, binary2);
        }
    }
}