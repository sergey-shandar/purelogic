using System;
using System.Collections.Generic;

namespace PureLogic
{
    public sealed class SelectMany<I, T> : Map<T>
    {
        public Bag<I> Input { get; }

        public Func<I, IEnumerable<T>> Func { get; }

        public SelectMany(Bag<I> input, Func<I, IEnumerable<T>> func)
        {
            Input = input;
            Func = func;
        }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
