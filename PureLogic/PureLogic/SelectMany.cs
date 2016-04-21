using System;
using System.Collections.Generic;

namespace PureLogic
{
    public sealed class SelectMany<I, T> : Map<T>
    {
        public readonly Bag<I> Input;

        public readonly Func<I, IEnumerable<T>> Func;

        public SelectMany(Bag<I> input, Func<I, IEnumerable<T>> func)
        {
            Input = input;
            Func = func;
        }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
