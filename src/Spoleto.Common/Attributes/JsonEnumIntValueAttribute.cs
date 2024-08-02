using System;

namespace Spoleto.Common.Attributes
{
    /// <summary>
    /// The attribute to overrie Json representation using <see cref="JsonConverters.JsonEnumIntValueConverter{T}"/>.
    /// </summary>
    /// <remarks>
    ///  Designed for enums.
    ///  </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class JsonEnumIntValueAttribute : Attribute
    {
        /// <summary>
        /// Constructor for nullable value.
        /// </summary>
        public JsonEnumIntValueAttribute()
        {
        }

        /// <summary>
        /// Constructor for non-nullable value.
        /// </summary>
        /// <param name="value">Int value</param>
        public JsonEnumIntValueAttribute(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the Json value for the enum item.
        /// </summary>
        public int? Value { get; }
    }
}
