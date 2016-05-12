using System;
using System.Collections.Generic;
using System.Linq;

namespace PureLogic
{
    public static class BinOperations
    {
        private struct Counters
        {
            public readonly long A;
            public readonly long B;

            public Counters(long a, long b)
            {
                A = a;
                B = b;
            }
        }

        private static Bag<KeyValuePair<T, Counters>> ToMultiSet<T>(
            this Bag<T> input, long a, long b)
            => input.Select(v => v.WithValue(new Counters(a, b)));

        private static Bag<KeyValuePair<T, Counters>> Dif<T>(this Bag<T> a, Bag<T> b)
            => a
                .ToMultiSet(1L, 0L)
                .DisjointUnion(b.ToMultiSet(0L, 1L))
                .GroupBy((x, y) => new Counters(x.A + y.A, x.B + y.B));

        private static Bag<T> DifSelectMany<T>(
            this Bag<T> a, Bag<T> b, Func<Counters, long> func)
            => a.Dif(b).SelectMany(v => Enumerable.Repeat(v.Key, (int)func(v.Value)));

        public static Bag<T> Except<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => v.A - v.B);

        public static Bag<T> Intersect<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => Math.Min(v.A, v.B));

        public static Bag<T> Union<T>(this Bag<T> a, Bag<T> b)
            => a.DifSelectMany(b, v => Math.Max(v.A, v.B));

        public static Bag<bool> BagEqual<T>(this Bag<T> a, Bag<T> b)
            => a.Dif(b).All(v => v.Value.A == v.Value.B);
    }
}
