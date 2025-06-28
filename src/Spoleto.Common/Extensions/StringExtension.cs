using System.Collections.Generic;

namespace Spoleto.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Splits a string into a list of substrings based on the specified separator.
        /// </summary>
        /// <param name="origin">The string to split.</param>
        /// <param name="separator">The separator string used to split the input string.</param>
        /// <returns>A list of strings representing the substrings extracted from the input string.</returns>
        public static List<string> SplitBySubstring(this string origin, string separator)
        {
            var result = new List<string>();
            var startIndex = 0;
            int index;

            // Loop through the string, finding occurrences of the separator.
            while ((index = origin.IndexOf(separator, startIndex)) != -1)
            {
                result.Add(origin.Substring(startIndex, index - startIndex));
                startIndex = index + separator.Length;
            }

            // Add the remaining portion of the string after the last separator.
            result.Add(origin.Substring(startIndex));

            return result;
        }
    }
}
