using System.Text.Json.Serialization;
using Spoleto.Common.Attributes;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    [JsonConverter(typeof(JsonEnumValueConverter<TestEnum>))]
    public enum TestEnum
    {
        [JsonEnumValue("O1")]
        One,

        [JsonEnumValue("T2")]
        Two,

        [JsonEnumValue(null)]
        Null
    }
}
