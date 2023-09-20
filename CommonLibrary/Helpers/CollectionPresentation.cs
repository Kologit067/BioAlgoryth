using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Helpers
{
    public static class CollectionPresentation
    {
        public static string AsString(this int[][] collection )
        {
            return String.Join("  ", collection.Select(e => $"({string.Join(",", e)})"));
        }
        public static string AsString(this List<List<int>> collection)
        {
            return String.Join("  ", collection.Select(e => $"({string.Join(",", e)})"));
        }
    }
}
