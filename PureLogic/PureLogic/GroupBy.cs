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

        public static Bag<T> Aggregate<T>(this Bag<T> input, Func<T, T, T> func)
            => input
                .Select(i => Tuple.Create(new Void(), i))
                .GroupBy(func)
                .Select(i => i.Item2);
    }
}
