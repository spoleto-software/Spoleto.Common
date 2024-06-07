using System;

namespace Spoleto.Common.Attributes
{
    /// <summary>
    /// The attribute to overrie Json representation using <see cref="JsonConverters.JsonEnumValueConverter{T}"/>.
    /// </summary>
    /// <remarks>
    ///  Designed for enums.
    ///  </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class JsonEnumValueAttribute : Attribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">String value, can be null.</param>
        public JsonEnumValueAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the Json value for the enum item.
        /// </summary>
        public string Value { get; }
    }
}
