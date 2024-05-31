using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Web;

namespace Spoleto.Common.Helpers
{
    /// <summary>
    /// The JSON Serializer with Cyrillic support based on System.Text.Json.
    /// </summary>
    public static class JsonHelper
    {
        private static readonly JavaScriptEncoder _encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
        private static readonly JsonSerializerOptions _defaultSerializerOptions;

        static JsonHelper()
        {
            _defaultSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = _encoder
            };

            _defaultSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Adds the custom converter for Json serializer.
        /// </summary>
        /// <param name="converter">The custom converter.</param>
        public static void AddConverter(JsonConverter converter)
        {
            if (!_defaultSerializerOptions.Converters.Contains(converter))
                _defaultSerializerOptions.Converters.Add(converter);
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static string ToJson<T>(T body, JsonSerializerOptions options = null)
        {
            if (body == null)
                return null;

            options ??= _defaultSerializerOptions;

            var bodyJson = JsonSerializer.Serialize(body, options);

            return bodyJson;
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static T FromJson<T>(string bodyJson, JsonSerializerOptions options = null)
        {
            if (string.IsNullOrEmpty(bodyJson))
                return default;

            options ??= _defaultSerializerOptions;

            var body = JsonSerializer.Deserialize<T>(bodyJson, options);

            return body;
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static string ToJson(object body, Type inputType, JsonSerializerOptions options = null)
        {
            options ??= _defaultSerializerOptions;

            var bodyJson = JsonSerializer.Serialize(body, inputType, options);

            return bodyJson;
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static object FromJson(string bodyJson, Type returnType, JsonSerializerOptions options = null)
        {
            options ??= _defaultSerializerOptions;

            var body = JsonSerializer.Deserialize(bodyJson, returnType, options);

            return body;
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static async Task ToJsonStreamAsync<T>(T body, Stream streamTo, JsonSerializerOptions options = null)
        {
            options ??= _defaultSerializerOptions;

            await JsonSerializer.SerializeAsync(streamTo, body, options).ConfigureAwait(false);
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static async Task<T> FromJsonStreamAsync<T>(Stream jsonStream, JsonSerializerOptions options = null)
        {
            options ??= _defaultSerializerOptions;

            var body = await JsonSerializer.DeserializeAsync<T>(jsonStream, options).ConfigureAwait(false);

            return body;
        }
    }
}
