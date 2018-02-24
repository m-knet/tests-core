// ReSharper disable once CheckNamespace
namespace DevOpsFlex.Tests.Core
{
    using System;
    using System.Collections.Generic;

    internal static class NumberExtensions
    {
        public static IEnumerable<int> To(this int from, int to)
        {
            if (to >= from)
            {
                for (var i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (var i = from; i >= to; i--)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<T> Times<T>(this int num, T toReturn)
        {
            for (var i = 0; i < num; i++)
            {
                yield return toReturn;
            }
        }

        public static IEnumerable<T> Times<T>(this int num, Func<int, T> block)
        {
            for (var i = 0; i < num; i++)
            {
                yield return block(i);
            }
        }
    }
}