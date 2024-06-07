using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;

namespace Spoleto.Common.JsonConverters
{
    /// <summary>
    /// Represents enums as their values of <see cref="JsonEnumValueAttribute"/> or names if <see cref="JsonEnumValueAttribute"/> is missed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonEnumValueConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value;
            if (reader.TokenType == JsonTokenType.Null)
            {
                value = null;
            }
            else
            {
                value = reader.GetString();
            }

            foreach (var field in typeof(T).GetFields())
            {
                if (field.GetCustomAttribute<JsonEnumValueAttribute>()?.Value == value)
                {
                    return (T)field.GetValue(null);
                }
            }

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                if (e.ToString() == value)
                {
                    return (T)e;
                }
            }

            throw new JsonException($"Unknown enum element: {value}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var enumValue = GetEnumValue(value);

            if (enumValue == null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(enumValue);
        }

        private string GetEnumValue(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            return field.GetCustomAttribute<JsonEnumValueAttribute>() is JsonEnumValueAttribute attribute
                ? attribute.Value
                : value.ToString();
        }
    }
}
