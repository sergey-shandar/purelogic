using System;

namespace PureLogic
{
    public sealed class GroupBy<K, T> : Reduce<Tuple<K, T>>
    {
        public Bag<Tuple<K, T>> Input { get; }

        public Func<T, T, T> Func { get; }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

        public GroupBy(Bag<Tuple<K, T>> input, Func<T, T, T> func)
        {
            Input = input;
            Func = func;
        }
    }

    public static class GroupByX
    {
        public static Bag<Tuple<K, T>> GroupBy<K, T>(
            this Bag<Tuple<K, T>> input, Func<T, T, T> func)
            => new GroupBy<K, T>(input, func);

        public static Bag<T> Aggregate<T>(this Bag<T> input, T initial, Func<T, T, T> func)
            => input
                .DisjointUnion(initial.Const())
                .Select(i => Tuple.Create(new Void(), i))
                .GroupBy(func)
                .Select(i => i.Item2);

        public static Bag<Option<T>> Aggregate<T>(this Bag<T> input, Func<T, T, T> func)
            => input
                .Select(OptionX.Option)
                .Aggregate(new Option<T>(), func.OptionBinaryFunc);

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

        public static Bag<long> Count<T>(this Bag<T> input)
            => input.Select(_ => 1L).Sum();

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
