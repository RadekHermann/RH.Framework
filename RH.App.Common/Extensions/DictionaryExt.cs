using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RH.App.Common.Extensions
{
    public static class DictionaryExt
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, [NotNull] TKey key, TValue value)
            where TKey : notnull
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
    }
}
