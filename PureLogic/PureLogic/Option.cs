using System;
using System.Collections.Generic;
using System.Linq;

namespace PureLogic
{
    public struct Option<T>
    {
        public readonly bool HasValue;

        public readonly T Value; 

        public Option(T value)
        {
            HasValue = true;
            Value = value;
        }
    }

    public static class OptionX
    {
        public static Option<T> Option<T>(this T value)
            => new Option<T>(value);

        public static R Apply<T, R>(this Option<T> option, Func<T, R> then, R else_)
            => option.HasValue ? then(option.Value) : else_;

        public static Option<R> SelectMany<T, R>(this Option<T> option, Func<T, Option<R>> func)
            => option.Apply(func, new Option<R>());
    
        public static Option<R> Select<T, R>(this Option<T> option, Func<T, R> func)
            => option.SelectMany(value => func(value).Option());

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> func)
            => option.SelectMany(value => func(value) ? option : new Option<T>());

        public static IEnumerable<T> ToEnum<T>(this Option<T> option)
            => option.Apply(value => new[] { value }, Enumerable.Empty<T>());
    }
}
