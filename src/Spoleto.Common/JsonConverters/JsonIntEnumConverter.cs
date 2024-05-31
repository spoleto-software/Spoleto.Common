using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.Common.JsonConverters
{
    public class JsonIntEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number || !reader.TryGetInt32(out var intValue))
            {
                throw new JsonException($"Unable to convert \"{reader.GetString()}\" to Enum \"{typeToConvert}\".");
            }

            return (T)(object)intValue;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)(object)value);
        }
    }
}
