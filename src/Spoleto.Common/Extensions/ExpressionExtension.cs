using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Spoleto.Common.Extensions
{
    /// <summary>
    /// Extensions for Linq expressions.
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// Converts the Linq expression into the Dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionaryValues<T>(this Expression<Func<T>> expr)
        {
            if (!(expr?.Body is MemberInitExpression memberInitExpression))
            {
                return null;
            }

            var dictionary = new Dictionary<string, object>();
            foreach (var binding in memberInitExpression.Bindings)
            {
                dictionary[binding.Member.Name] = binding.GetValue();
            }

            return dictionary;
        }

        public static object GetValue(this MemberBinding binding)
        {
            if (binding.BindingType == MemberBindingType.Assignment)
            {
                var memberAssignment = (MemberAssignment)binding;
                if (memberAssignment.Expression is ConstantExpression constantExpression)
                {
                    return constantExpression.Value;
                }

                try
                {
                    return memberAssignment.Expression.ExecuteExpression();
                }
                catch (Exception)
                {
                    return Expression.Lambda<Func<object>>(Expression.Convert(memberAssignment.Expression, typeof(object)), Array.Empty<ParameterExpression>()).Compile()();
                }
            }

            return null;
        }

        public static object ExecuteExpression(this Expression initialExpression)
        {
            var e = initialExpression;
            if (initialExpression.NodeType == ExpressionType.Lambda)
            {
                if (((LambdaExpression)initialExpression).Body.NodeType == ExpressionType.Constant)
                    return ((ConstantExpression)((LambdaExpression)initialExpression).Body).Value;

                e = ((LambdaExpression)initialExpression).Body;
            }

            var member = Expression.Convert(e, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            var value = getter();

            return value;
        }
    }
}
