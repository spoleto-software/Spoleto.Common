using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.Common.JsonConverters
{
    /// <summary>
    /// Represents enums as their values of <see cref="DescriptionAttribute"/> or names if <see cref="DescriptionAttribute"/> is missed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDescriptionEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            foreach (var field in typeof(T).GetFields())
            {
                if (field.GetCustomAttribute<DescriptionAttribute>()?.Description == value)
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
            var description = GetEnumDescription(value);

            writer.WriteStringValue(description);
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            return field.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute
                ? attribute.Description
                : value.ToString();
        }
    }
}
