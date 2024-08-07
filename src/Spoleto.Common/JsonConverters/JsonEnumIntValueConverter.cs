﻿using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;

namespace Spoleto.Common.JsonConverters
{
    /// <summary>
    /// Represents enums as their values of <see cref="JsonEnumIntValueAttribute"/> or names if <see cref="JsonEnumIntValueAttribute"/> is missed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonEnumIntValueConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int? value;
            if (reader.TokenType == JsonTokenType.Null)
            {
                value = null;
            }
            else
            {
                value = reader.GetInt32();
            }

            foreach (var field in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (field.GetCustomAttribute<JsonEnumIntValueAttribute>()?.Value == value)
                {
                    return (T)field.GetValue(null);
                }
            }

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(e) == value)
                {
                    return (T)e;
                }
            }

            if (value == 0)
            {
                return default;
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
                    writer.WriteNumberValue(enumValue.Value);
            }
            else
            {
                writer.WriteNumberValue((int)(object)value);
            }
        }

        private bool TryGetEnumValue(Enum value, out int? outValue)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                outValue = null;
                return false;
            }

            outValue = field.GetCustomAttribute<JsonEnumIntValueAttribute>() is JsonEnumIntValueAttribute attribute
                ? attribute.Value
                : Convert.ToInt32(value);

            return true;
        }
    }
}
