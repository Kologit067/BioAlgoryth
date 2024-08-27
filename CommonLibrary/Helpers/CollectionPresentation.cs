using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary.Helpers
{
    //--------------------------------------------------------------------------------------
    // class CollectionPresentation
    //--------------------------------------------------------------------------------------
    public static class CollectionPresentation
    {
        //--------------------------------------------------------------------------------------
        public static string AsString(this int[][] collection )
        {
            return String.Join("  ", collection.Select(e => $"({string.Join(",", e)})"));
        }
        //--------------------------------------------------------------------------------------
        public static string AsString(this List<List<int>> collection)
        {
            return String.Join("  ", collection.Select(e => $"({string.Join(",", e)})"));
        }
        //--------------------------------------------------------------------------------------
        public static int[][] StringToArray(string pListOfSetAsString)
        {
            string[] clauseArray = pListOfSetAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < clauseArray.Length; i++)
            {
                string clause = clauseArray[i];
                string[] vertexArray = clause.Replace("(", "").Replace(")", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                result.Add(vertexArray.Where(v => int.TryParse(v, out _)).Select(v => int.Parse(v)).ToArray());
            }
            return result.ToArray();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
