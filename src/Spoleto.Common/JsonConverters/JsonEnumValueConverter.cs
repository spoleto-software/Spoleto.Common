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
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var numValue = reader.GetInt32();
                if (numValue == 0)
                    return default;

                throw new JsonException($"Unknown enum '{typeof(T).Name}' element: {numValue}");
            }
            else
            {
                value = reader.GetString();
            }

            foreach (var field in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
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

            throw new JsonException($"Unknown enum '{typeof(T).Name}' element: {value}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (TryGetEnumValue(value, out var enumValue))
            {
                if (enumValue == null)
                    writer.WriteNullValue();
                else
                    writer.WriteStringValue(enumValue);
            }
            else
            {
                writer.WriteNumberValue((int)(object)value);
            }
        }

        private bool TryGetEnumValue(Enum value, out string outValue)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                outValue = null;
                return false;
            }

            outValue = field.GetCustomAttribute<JsonEnumValueAttribute>() is JsonEnumValueAttribute attribute
                ? attribute.Value
                : value.ToString();

            return true;
        }
    }
}
