using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    [JsonConverter(typeof(JsonEnumIntValueConverter<TestEnumIntValue>))]
    public enum TestEnumIntValue
    {
        [JsonEnumIntValue(100)]
        One = 100,

        [JsonEnumIntValue(200)]
        Two = 200,

        [JsonEnumIntValue]
        Null = -1
    }
}
