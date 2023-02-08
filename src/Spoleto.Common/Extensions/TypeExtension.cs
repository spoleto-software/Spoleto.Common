using System;
using System.Linq;
using System.Reflection;

namespace Spoleto.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtension
    {
        public static T FirstAttribute<T>(this Type type) where T : Attribute
        {
            return (T)type.GetCustomAttributes(typeof(T), inherit: true).FirstOrDefault();
        }

        public static T FirstAttribute<T>(this PropertyInfo type) where T : Attribute
        {
            return (T)type.GetCustomAttributes(typeof(T), inherit: true).FirstOrDefault();
        }

        /// <summary>
        /// Determinates is the type nullable.
        /// </summary>
        public static bool IsNullableType(this Type theType)
        {
            return (theType.IsGenericType
                && theType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Gets the real type: if type is nullable it will be underlying type.
        /// </summary>
        public static Type GetRealType(this Type type)
        {
            if (type == null)
            {
                return null;
            }

            var isNullableType = type.IsNullableType();

            return isNullableType
                ? Nullable.GetUnderlyingType(type)
                : type;
        }

        /// <summary>
        /// Get empty value.
        /// </summary>
        public static object GetEmptyValue(this Type type)
        {
            var tempres = _getEmptyValueMethod.MakeGenericMethod(type).Invoke(null, new object[0]);
            return tempres;
        }

        private static readonly MethodInfo _getEmptyValueMethod = typeof(TypeExtension).GetMethod(nameof(GetEmptyValue), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        private static object GetEmptyValue<T>()
        {
            if (typeof(T) == typeof(string))
                return String.Empty;
                    
            return default(T);
        }
    }
}
