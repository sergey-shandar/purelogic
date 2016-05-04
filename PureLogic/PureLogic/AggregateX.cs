using System;

namespace PureLogic
{
    public static class AggregateX
    {
        public static Bag<T> Aggregate<T>(this Bag<T> input, T initial, Func<T, T, T> func)
            => input
                .DisjointUnion(initial.Const())
                .Select(i => new Void().WithValue(i))
                .GroupBy(func)
                .Select(i => i.Value);

        public static Bag<Option<T>> Aggregate<T>(this Bag<T> input, Func<T, T, T> func)
            => input
                .Select(OptionX.Option)
                .Aggregate(new Option<T>(), func.OptionBinaryFunc);
    }
}
