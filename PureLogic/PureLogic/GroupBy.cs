using System;
using System.Collections.Generic;
using System.Linq;

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

        public static Bag<T> Distinct<T>(this Bag<T> input)
            => input
                .Select(v => v.WithValue(new Void()))
                .GroupBy((a, _) => a)
                .Select(v => v.Key);
    }
}
