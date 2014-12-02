using System;
using System.Collections.Generic;
using System.Linq;

namespace PiggySync.Common
{
    public static class Extensions
    {
        public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T) item.Clone()).ToList();
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

		public static int CountSubstrings(this string str, string subStr){
			int count = 0;
			int i = 0;
			while ((i = str.IndexOf(subStr, i)) != -1)
			{
				i += subStr.Length;
				count++;
			}
			return count;
		}
    }
}