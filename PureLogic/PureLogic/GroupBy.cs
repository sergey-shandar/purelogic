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
}
