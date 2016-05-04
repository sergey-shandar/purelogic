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
}
