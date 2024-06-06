using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.Common.JsonConverters
{
    public abstract class JsonDateTimeConverterBase : JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat;

        protected JsonDateTimeConverterBase(string dateTimeFormat)
        {
            if (dateTimeFormat == null)
                throw new ArgumentNullException(nameof(dateTimeFormat));

            _dateTimeFormat = dateTimeFormat;
        }
        
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, _dateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));
        }
    }
}
