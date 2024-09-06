using System.Text.Json.Serialization;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    public class TestClass
    {
        [JsonConverter(typeof(JsonEnumValueConverter<TestEnumValue>))]
        public TestEnumValue Test1 { get; set; }

        [JsonConverter(typeof(JsonEnumValueConverter<TestEnumValue>))]
        public TestEnumValue Test2 { get; set; }

        [JsonConverter(typeof(JsonIntEnumConverter<TestIntEnum>))]
        public TestIntEnum TestIntEnum { get; set; }

        [JsonConverter(typeof(JsonEnumIntValueConverter<TestEnumIntValue>))]
        public TestEnumIntValue TestEnumIntValue1 { get; set; }

        [JsonConverter(typeof(JsonEnumIntValueConverter<TestEnumIntValue>))]
        public TestEnumIntValue TestEnumIntValue2 { get; set; }

        [JsonConverter(typeof(JsonStrIntEnumConverter<TestIntEnum>))]
        public TestIntEnum TestIntEnumAsStrInt { get; set; }
    }
}