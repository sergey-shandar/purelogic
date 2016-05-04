using System;

namespace PureLogic
{
    public static class SelectManyX
    {
        public static Bag<T> Select<I, T>(this Bag<I> input, Func<I, T> func)
            => null;

        public static Bag<T> Where<T>(this Bag<T> input, Func<T, bool> func)
            => null;
    }
}
