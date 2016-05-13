using System;
using System.Collections.Generic;
using System.Linq;

namespace PureLogic
{
    using Counters = GroupByX.Pair<long, long>;

    public static class BinOperations
    {
        private static Bag<KeyValuePair<T, Counters>> ToMultiSet<T>(
            this Bag<T> input, long a, long b)
            => input.Select(v => v.WithValue(new Counters(a, b)));

        private static readonly Func<long, long, long> longPlus = (a, b) => a + b;

        private static Bag<KeyValuePair<T, Counters>> Dif<T>(this Bag<T> a, Bag<T> b)
            => a
                .ToMultiSet(1L, 0L)
                .DisjointUnion(b.ToMultiSet(0L, 1L))
                .GroupBy(longPlus.PairBinaryFunc(longPlus));

        private static Bag<T> DifSelectMany<T>(
            this Bag<T> a, Bag<T> b, Func<Counters, long> func)
            => a.Dif(b).SelectMany(v => Enumerable.Repeat(v.Key, (int)func(v.Value)));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb300779(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bag<T> Except<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => v.A - v.B);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb460136(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bag<T> Intersect<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => Math.Min(v.A, v.B));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb341731(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bag<T> Union<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => Math.Max(v.A, v.B));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb348567(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bag<bool> BagEqual<T>(this Bag<T> a, Bag<T> b)
            => a.Dif(b).All(v => v.Value.A == v.Value.B);
    }
}
