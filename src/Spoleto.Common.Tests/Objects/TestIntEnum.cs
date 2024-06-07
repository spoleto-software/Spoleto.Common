using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    [JsonConverter(typeof(JsonIntEnumConverter<TestIntEnum>))]
    public enum TestIntEnum
    {
        [JsonEnumValue("O1")]
        One = 100,

        [JsonEnumValue("T2")]
        Two = 200,

        [JsonEnumValue(null)]
        Null = -1
    }
}
