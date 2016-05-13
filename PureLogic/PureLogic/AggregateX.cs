using System;

namespace PureLogic
{
    public static class AggregateX
    {
        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb549218(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="initial"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Bag<T> Aggregate<T>(this Bag<T> input, T initial, Func<T, T, T> func)
            => input
                .DisjointUnion(initial.ToBag())
                .Select(i => new Void().WithValue(i))
                .GroupBy(func)
                .Select(i => i.Value);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb548651(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Bag<Option<T>> Aggregate<T>(this Bag<T> input, Func<T, T, T> func)
            => input
                .Select(OptionX.Option)
                .Aggregate(new Option<T>(), func.OptionBinaryFunc());

        public static Bag<bool> AllTrue(this Bag<bool> input)
            => input.Aggregate(true, (a, b) => a && b);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb548541(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Bag<bool> All<T>(this Bag<T> input, Func<T, bool> f)
            => input.Select(f).AllTrue();

        public static Bag<bool> AnyTrue(this Bag<bool> input)
            => input.Aggregate(false, (a, b) => a || b);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb534972(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Bag<bool> Any<T>(this Bag<T> input, Func<T, bool> f)
            => input.Select(f).AnyTrue();

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb337697(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<bool> Any<T>(this Bag<T> input)
            => input.Any(_ => true);

        /// <summary>
        /// Se also https://msdn.microsoft.com/en-us/library/bb352880(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Bag<bool> Contains<T>(this Bag<T> input, T value)
            => input.Any(v => v.Equals(value));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb360179(v=vs.110).aspx
        /// and https://msdn.microsoft.com/en-us/library/bb355419(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Bag<T> DefaultIfEmpty<T>(this Bag<T> input, T value = default(T))
            => input
                .Any()
                .Where(any => !any)
                .Select(_ => value)
                .DisjointUnion(input);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb352408(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<Option<T>> Min<T>(this Bag<T> input)
            where T : IComparable
            => input.Aggregate((a, b) => a.CompareTo(b) <= 0 ? a : b);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb347632(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<Option<T>> Max<T>(this Bag<T> input)
            where T : IComparable
            => input.Aggregate((a, b) => a.CompareTo(b) >= 0 ? a : b);

        public static Bag<Option<T>> Average<P, T>(this P policy, Bag<T> input)
            where P : struct, INumericPolicy<T>
            => input
                .Select(v => Tuple.Create(v, 1L))
                .Aggregate((a, b) => Tuple.Create(policy.Plus(a.Item1, b.Item1), a.Item2 + b.Item2))
                .Select(x => x.Select(v => policy.Div(v.Item1, policy.FromLong(v.Item2))));

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb358946(v=vs.110).aspx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<Option<double>> Average(this Bag<double> input)
            => new NumericPolicy().Average(input);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb354760(v=vs.110).aspx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<Option<decimal>> Average(this Bag<decimal> input)
            => new NumericPolicy().Average(input);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb338038(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<long> Count<T>(this Bag<T> input)
            => input.Select(_ => 1L).Sum();

        public static Bag<T> Sum<P, T>(this P policy, Bag<T> input)
            where P : struct, INumericPolicy<T>
            => input.Aggregate(policy.FromLong(0), policy.Plus);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb337919(v=vs.110).aspx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<long> Sum(this Bag<long> input)
            => new NumericPolicy().Sum(input);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb298381(v=vs.110).aspx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<double> Sum(this Bag<double> input)
            => new NumericPolicy().Sum(input);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb298138(v=vs.110).aspx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bag<decimal> Sum(this Bag<decimal> input)
            => new NumericPolicy().Sum(input);
    }
}
