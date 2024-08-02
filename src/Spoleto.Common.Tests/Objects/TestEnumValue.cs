using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    [JsonConverter(typeof(JsonEnumValueConverter<TestEnumValue>))]
    public enum TestEnumValue
    {
        [JsonEnumValue("O1")]
        One = 1,

        [JsonEnumValue("T2")]
        Two = 2,

        [JsonEnumValue(null)]
        Null = -1
    }
}
