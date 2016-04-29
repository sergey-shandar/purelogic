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

        public static Bag<int> Sum(this Bag<int> input)
            => input.Aggregate(0, (a, b) => a + b);

        public static Bag<long> Sum(this Bag<long> input)
            => input.Aggregate(0, (a, b) => a + b);

        public static Bag<float> Sum(this Bag<float> input)
            => input.Aggregate(0, (a, b) => a + b);

        public static Bag<double> Sum(this Bag<double> input)
            => input.Aggregate(0, (a, b) => a + b);

        public static Bag<decimal> Sum(this Bag<decimal> input)
            => input.Aggregate(0, (a, b) => a + b);
    }
}
