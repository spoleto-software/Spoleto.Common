using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spoleto.Common.Helpers
{
    /// <summary>
    /// The helper for serialization of types.
    /// </summary>
    public static class SerializedTypeHelper
    {
        /// <summary>
        /// Custom binder type.
        /// </summary>
        public static Func<string, Type> BindToType { get; set; } = DefaultBindToType;

        /// <summary>
        /// Gets the type name does not have Version, Culture, Public token.
        /// </summary>
        public static bool RemoveAssemblyVersion = true;

        private static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);

        // mscorlib or System.Private.CoreLib
        private static readonly bool IsMscorlib = typeof(int).AssemblyQualifiedName.Contains("mscorlib");

        private static readonly Dictionary<string, Type> TypeCache = [];

        /// <summary>
        /// Deserializes the type.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type DeserializeType(string typeName)
        {
            lock (((ICollection)TypeCache).SyncRoot)
            {
                if (!TypeCache.TryGetValue(typeName, out var type))
                {
                    type = BindToType(typeName);
                    if (type == null)
                    {
                        if (IsMscorlib && typeName.Contains("System.Private.CoreLib"))
                        {
                            typeName = typeName.Replace("System.Private.CoreLib", "mscorlib");
                            type = Type.GetType(typeName);
                        }
                        else if (!IsMscorlib && typeName.Contains("mscorlib"))
                        {
                            typeName = typeName.Replace("mscorlib", "System.Private.CoreLib");
                            type = Type.GetType(typeName);
                        }
                        else
                        {
                            type = Type.GetType(typeName);
                        }
                    }

                    if (type == null)
                    {
                        throw new TypeLoadException($"Cannot load type by the name <{typeName}>.");
                    }

                    TypeCache[typeName] = type;
                }

                return type;
            }
        }

        /// <summary>
        /// Builds the type name for serialization.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string BuildTypeName(Type type)
        {
            if (RemoveAssemblyVersion)
            {
                string full = type.AssemblyQualifiedName;

                var shortened = SubtractFullNameRegex.Replace(full, String.Empty);
                if (Type.GetType(shortened) == null)
                {
                    // if type cannot be found with shortened name - use full name
                    shortened = full;
                }

                return shortened;
            }
            else
            {
                return type.AssemblyQualifiedName;
            }
        }

        private static Type DefaultBindToType(string typeName) => Type.GetType(typeName);
    }
}
