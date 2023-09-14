namespace Senthil.WAE.Assignment.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FileUtility
    {
        public static string ToComma<T, TU>(this IEnumerable<T> source, Func<T, TU> func)
        {
            return string.Join("\r\n", source.Select(s => func(s).ToString()).ToArray());
        }
    }
}