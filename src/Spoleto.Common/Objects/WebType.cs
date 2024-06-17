using System;
using System.Runtime.Serialization;
using Spoleto.Common.Helpers;

namespace Spoleto.Common.Objects
{
    /// <summary>
    /// String representation of Type.
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebType
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WebType()
        {
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="type"></param>
        public WebType(Type type)
        {
            if (type != null)
            {
                TypeName = SerializedTypeHelper.BuildTypeName(type);
            }
        }

        /// <summary>
        /// Full type name with assembly name.
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets the real type.
        /// </summary>
        public Type GetRealType() => TypeName != null ? SerializedTypeHelper.DeserializeType(TypeName) : null;

        /// <summary>
        ///  User-defined conversion from Type to WebType.
        /// </summary>
        /// <param name="type"></param>
        public static explicit operator WebType(Type type)
        {
            return new WebType(type);
        }

        /// <summary>
        /// User-defined conversion from WebType to Type.
        /// </summary>
        /// <param name="webType"></param>
        public static explicit operator Type(WebType webType)
        {
            return webType?.GetRealType();
        }

        /// <summary>
        /// Returns the string representation.
        /// </summary>
        /// <returns></returns>
        public override String ToString() => TypeName;
    }
}
