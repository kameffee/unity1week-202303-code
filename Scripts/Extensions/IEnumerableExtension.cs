using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity1week202303.Extensions
{
    public static class IEnumerableExtension
    {
        public static IOrderedEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}
