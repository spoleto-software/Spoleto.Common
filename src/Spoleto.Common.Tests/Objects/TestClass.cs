﻿using System.Text.Json.Serialization;
using Spoleto.Common.JsonConverters;

namespace Spoleto.Common.Tests
{
    public class TestClass
    {
        [JsonConverter(typeof(JsonEnumValueConverter<TestEnum>))]
        public TestEnum Test { get; set; }

        [JsonConverter(typeof(JsonIntEnumConverter<TestIntEnum>))]
        public TestIntEnum TestIntEnum { get; set; }
    }
}