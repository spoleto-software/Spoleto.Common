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
    /// The HTTP helper.
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Converts the given object to HTTP query string.
        /// </summary>
        public static string ToQueryString<T>(T body)
        {
            var bodyJson = JsonHelper.ToJson(body);
            var dictionaryAsObjectValues = JsonHelper.FromJson<Dictionary<string, object>>(bodyJson);

            var args = new List<string>();
            foreach (var key in dictionaryAsObjectValues.Keys)
            {
                var jsonValue = (JsonElement)dictionaryAsObjectValues[key];
                var objValue = FlattenJsonValue(jsonValue);
                if (objValue is string str)
                {
                    args.Add($"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(str)}");
                }
                else if (objValue is IEnumerable enumerable)
                {
                    foreach (string item in enumerable)
                    {
                        args.Add($"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(item)}");
                    }
                }
            }

            return string.Join("&", args);
        }

        private static object FlattenJsonValue(JsonElement objValue)
        {
            return objValue.ValueKind switch
            {
                JsonValueKind.String => objValue.GetString(),
                JsonValueKind.Array => objValue.EnumerateArray().Select(FlattenJsonValue),
                _ => objValue.GetRawText()
            };
        }
    }
}
