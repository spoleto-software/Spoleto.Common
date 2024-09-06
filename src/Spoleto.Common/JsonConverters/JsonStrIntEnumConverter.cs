using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.Common.JsonConverters
{
    /// <summary>
    /// In can parse from string and from numbers but write as string only.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonStrIntEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var strInt = reader.GetString();
                if (int.TryParse(strInt, out var result))
                    return (T)(object)result;
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
            {
                return (T)(object)intValue;
            }

            throw new JsonException($"Unable to convert to Enum \"{typeToConvert}\".");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
