using System;
using System.Collections.Generic;

namespace PureLogic
{
    public sealed class GroupBy<K, T> : Reduce<KeyValuePair<K, T>>
    {
        public Bag<KeyValuePair<K, T>> Input { get; }

        public Func<T, T, T> Func { get; }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

        public GroupBy(Bag<KeyValuePair<K, T>> input, Func<T, T, T> func)
        {
            Input = input;
            Func = func;
        }
    }

    public static class GroupByX
    {
        public static Bag<KeyValuePair<K, T>> GroupBy<K, T>(
            this Bag<KeyValuePair<K, T>> input, Func<T, T, T> func)
            => new GroupBy<K, T>(input, func);

        public static Bag<KeyValuePair<K, T>> GroupBy<K, T>(
            this Bag<T> input, Func<T, K> key, Func<T, T, T> reduce)
            => input.Select(v => key(v).WithValue(v)).GroupBy(reduce);

        public static Bag<bool> All(this Bag<bool> input)
            => input.Aggregate(true, (a, b) => a && b);

        public static Bag<bool> Any(this Bag<bool> input)
            => input.Aggregate(false, (a, b) => a || b);

        public static Bag<Option<T>> Average<P, T>(this P policy, Bag<T> input)
            where P : struct, INumericPolicy<T>
            => input
                .Select(v => Tuple.Create(v, 1L))
                .Aggregate((a, b) => Tuple.Create(policy.Plus(a.Item1, b.Item1), a.Item2 + b.Item2))
                .Select(x => x.Select(v => policy.Div(v.Item1, policy.FromLong(v.Item2))));

        public static Bag<Option<double>> Average(this Bag<double> input)
            => new NumericPolicy().Average(input);

        public static Bag<Option<decimal>> Average(this Bag<decimal> input)
            => new NumericPolicy().Average(input);

        public static Bag<bool> Contains<T>(this Bag<T> input, T value)
            => input.Where(v => v.Equals(value)).Select(_ => true);

        public static Bag<long> Count<T>(this Bag<T> input)
            => input.Select(_ => 1L).Sum();

        public static Bag<T> DefaultIfEmpty<T>(this Bag<T> input, T value = default(T))
            => input
                .Select(_ => true)
                .Any()
                .Where(any => !any)
                .Select(_ => value)
                .DisjointUnion(input);

        public static Bag<T> Distinct<T>(this Bag<T> input)
            => input
                .Select(v => v.WithValue(new Void()))
                .GroupBy((a, _) => a)
                .Select(v => v.Key);

        private static Bag<KeyValuePair<T, Tuple<long, long>>> ToMultiSet<T>(
            this Bag<T> input, long a, long b)
            => input.Select(v => v.WithValue(Tuple.Create(a, b)));

        private static Bag<KeyValuePair<T, Tuple<long, long>>> Dif<T>(this Bag<T> a, Bag<T> b)
            => a
                .ToMultiSet(1L, 0L)
                .DisjointUnion(b.ToMultiSet(0L, 1L))
                .GroupBy((x, y) => Tuple.Create(x.Item1 + y.Item1, x.Item2 + y.Item2));

        private static Bag<T> DifWhere<T>(
            this Bag<T> a, Bag<T> b, Func<Tuple<long, long>, bool> func)
            => a.Dif(b)
                .Where(v => func(v.Value))
                .Select(v => v.Key);

        public static Bag<T> Except<T>(this Bag<T> a, Bag<T> b)
            => a.DifWhere(b, v => v.Item1 == 0 || v.Item2 == 0);

        public static Bag<T> Intersect<T>(this Bag<T> a, Bag<T> b)
            => a.DifWhere(b, v => v.Item1 > 0 && v.Item2 > 0);

        public static Bag<T> Union<T>(this Bag<T> a, Bag<T> b)
            => a.Dif(b).Select(v => v.Key);

        public static Bag<T> Sum<P, T>(this P policy, Bag<T> input)
            where P : struct, INumericPolicy<T>
            => input.Aggregate(policy.FromLong(0), policy.Plus);

        public static Bag<long> Sum(this Bag<long> input)
            => new NumericPolicy().Sum(input);

        public static Bag<double> Sum(this Bag<double> input)
            => new NumericPolicy().Sum(input);

        public static Bag<decimal> Sum(this Bag<decimal> input)
            => new NumericPolicy().Sum(input);
    }
}
