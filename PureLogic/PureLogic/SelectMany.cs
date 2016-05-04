using System;
using System.Collections.Generic;
using System.Linq;

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

    public static class SelectManyX
    {
        public static Bag<T> SelectMany<I, T>(this Bag<I> input, Func<I, IEnumerable<T>> func)
            => new SelectMany<I, T>(input, func);

        public static Bag<T> Select<I, T>(this Bag<I> input, Func<I, T> func)
            => input.SelectMany(i => new[] { func(i) });

        public static Bag<T> Where<T>(this Bag<T> input, Func<T, bool> func)
            => input.SelectMany(i => func(i) ? Enumerable.Empty<T>() : new[] { i });

        /*
        p8ublic static Bag<T> OfType<T>(this Bag input)
            => input.Accept(new OfTypeVisitor<T>());

        private sealed class OfTypeVisitor<R> : Bag.IVisitor<Bag<R>>
        {
            public Bag<R> Visit<T>(Bag<T> bag)
                => bag.SelectMany(i => i is R ? new[] { (R)(object)i } : Enumerable.Empty<R>());
        }
        */
    }
}
