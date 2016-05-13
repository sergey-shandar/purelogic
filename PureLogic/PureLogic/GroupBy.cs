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

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb549393(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="reduce"></param>
        /// <returns></returns>
        public static Bag<KeyValuePair<K, T>> GroupBy<K, T>(
            this Bag<T> input, Func<T, K> key, Func<T, T, T> reduce)
            => input.GroupBy(key, v => v, reduce);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb534493(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reduce"></param>
        /// <returns></returns>
        public static Bag<KeyValuePair<K, V>> GroupBy<K, V, T>(
            this Bag<T> input, Func<T, K> key, Func<T, V> value, Func<V, V, V> reduce)
            => input.Select(v => key(v).WithValue(value(v))).GroupBy(reduce);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb348436(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<T> Distinct<T>(this Bag<T> input)
            => input
                .Select(v => v.WithValue(new Void()))
                .GroupBy((a, _) => a)
                .Select(v => v.Key);

        public struct Pair<TA, TB>
        {
            public readonly TA A;
            public readonly TB B;

            public Pair(TA a, TB b)
            {
                A = a;
                B = b;
            }
        }

        public static Pair<A, B> WithB<A, B>(this A a, B b)
            => new Pair<A, B>(a, b);

        public static Func<Pair<A, B>, Pair<A, B>, Pair<A, B>> PairBinaryFunc<A, B>(
            this Func<A, A, A> a, Func<B, B, B> b)
            => (x, y) => a(x.A, y.A).WithB(b(x.B, y.B));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb534675(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="reduceA"></param>
        /// <param name="reduceB"></param>
        /// <returns></returns>
        public static Bag<KeyValuePair<K, Pair<Option<A>, Option<B>>>> Join<K, A, B>(
            this Bag<KeyValuePair<K, A>> a,
            Bag<KeyValuePair<K, B>> b,
            Func<A, A, A> reduceA,
            Func<B, B, B> reduceB)
        {
            var ap = a.SelectValue(p => p.Option().WithB(new Option<B>()));
            var bp = b.SelectValue(p => new Option<A>().WithB(p.Option()));
            var ab = ap.DisjointUnion(bp);
            var r = reduceA.OptionBinaryFunc().PairBinaryFunc(reduceB.OptionBinaryFunc());
            return ab.GroupBy(r);
        }

        public static Bag<Pair<Option<A>, Option<B>>> Join<K, A, B>(
            this Bag<A> a,
            Bag<B> b,
            Func<A, K> aKey,
            Func<B, K> bKey,
            Func<A, A, A> aReduce,
            Func<B, B, B> bReduce)
        {
            var ak = a.SelectWithKey(aKey);
            var bk = b.SelectWithKey(bKey);
            return ak.Join(bk, aReduce, bReduce).Select(v => v.Value);
        }

        public static Bag<KeyValuePair<K, Pair<A, B>>> Left<K, A, B>(
            this Bag<KeyValuePair<K, Pair<Option<A>, B>>> bag)
            => bag.SelectValue(v => v.A.Select(a => a.WithB(v.B))).WhereValue();

        public static Bag<KeyValuePair<K, Pair<A, B>>> Right<K, A, B>(
            this Bag<KeyValuePair<K, Pair<A, Option<B>>>> bag)
            => bag.SelectValue(v => v.B.Select(b => v.A.WithB(b))).WhereValue();

        public static Bag<KeyValuePair<K, Pair<A, B>>> Inner<K, A, B>(
            this Bag<KeyValuePair<K, Pair<Option<A>, Option<B>>>> bag)
            => bag.Left().Right();
    }
}
