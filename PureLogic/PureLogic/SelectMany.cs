using System;
using System.Collections.Generic;
using System.Linq;

namespace PureLogic
{
    public abstract class SelectMany<I, T> : Map<T>
    {
    }

    public static class SelectManyX
    {
        public static Bag<T> SelectMany<I, T>(this Bag<I> input, Func<I, IEnumerable<T>> func)
            => null;

        public static Bag<T> Select<I, T>(this Bag<I> input, Func<I, T> func)
            => input.SelectMany(i => new[] { func(i) });

        public static Bag<T> Where<T>(this Bag<T> input, Func<T, bool> func)
            => input.SelectMany(i => func(i) ? Enumerable.Empty<T>() : new[] { i });
    }
}
