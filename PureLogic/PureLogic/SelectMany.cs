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
        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb534336(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Bag<T> SelectMany<I, T>(this Bag<I> input, Func<I, IEnumerable<T>> func)
            => new SelectMany<I, T>(input, func);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb548891(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Bag<T> Select<I, T>(this Bag<I> input, Func<I, T> func)
            => input.SelectMany(i => new[] { func(i) });

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb534803(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Bag<T> Where<T>(this Bag<T> input, Func<T, bool> func)
            => input.SelectMany(i => func(i) ? Enumerable.Empty<T>() : new[] { i });

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb360913(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<T> OfType<T>(this Bag input)
            => input.Accept(new OfTypeVisitor<T>());

        private sealed class OfTypeVisitor<R> : Bag.IVisitor<Bag<R>>
        {
            public Bag<R> Visit<T>(Bag<T> bag)
                => bag.SelectMany(i => i is R ? new[] { (R)(object)i } : Enumerable.Empty<R>());
        }

        public static Bag<KeyValuePair<K, T>> SelectValue<K, I, T>(
            this Bag<KeyValuePair<K, I>> bag, Func<I, T> f)
            => bag.Select(v => v.Key.WithValue(f(v.Value)));

        public static Bag<KeyValuePair<K, T>> WhereValue<K, T>(
            this Bag<KeyValuePair<K, Option<T>>> bag)
            => bag.SelectMany(p => p.Value.Select(v => p.Key.WithValue(v)).ToEnum());
    }
}
