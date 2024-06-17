using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spoleto.Common.Helpers;

namespace Spoleto.Common.JsonConverters
{
    /// <summary>
    /// The custom Json converter for the <see cref="Type"/>.
    /// </summary>
    public class JsonTypeConverter : JsonConverter<Type>
    {
        /// <summary>
        /// The default constructor to initialize a Json converter for the <see cref="Type"/>. 
        /// </summary>
        public JsonTypeConverter()
        {
        }

        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            var typeName = reader.GetString();

            if (typeName == null)
                return null;

            var resolvedType = SerializedTypeHelper.DeserializeType(typeName);

            if (resolvedType == null)
            {
                throw new JsonException($"Unable to resolve type {typeName}");
            }

            return resolvedType;
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            // Write the assembly-qualified name
            var typeName = SerializedTypeHelper.BuildTypeName(value);
            writer.WriteStringValue(typeName);
        }
    }
}
