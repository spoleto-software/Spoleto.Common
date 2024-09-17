using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace Spoleto.Common.Helpers
{
    /// <summary>
    /// The HTTP helper.
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Converts the given object to the HTTP query string.
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

        /// <summary>
        /// Converts the given object to the string <see cref="Dictionary{String, String}"/>.
        /// </summary>
        public static Dictionary<string, string> ToStringDictionary<T>(T body)
        {
            var bodyJson = JsonHelper.ToJson(body);
            var dictionaryAsObjectValues = JsonHelper.FromJson<Dictionary<string, object>>(bodyJson);

            var dictionary = new Dictionary<string, string>();
            foreach (var key in dictionaryAsObjectValues.Keys)
            {
                var jsonValue = (JsonElement)dictionaryAsObjectValues[key];
                var objValue = FlattenJsonValue(jsonValue);
                if (objValue is string str)
                {
                    dictionary.Add(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(str));
                }
                else if (objValue is IEnumerable enumerable)
                {
                    var count = 0;
                    foreach (string item in enumerable)
                    {
                        if (count++ > 0)
                            throw new ArgumentException("Cannot use IEnumerable argument with more than 1 element to convert to the Dictionary.");

                        dictionary.Add(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(item));
                    }
                }
            }

            return dictionary;
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
