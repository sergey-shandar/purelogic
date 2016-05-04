using System;

namespace PureLogic
{
    public static class SelectManyX
    {
        public static Bag Select(this Bag input, Func<int, int> func)
            => null;

        public static Bag Where(this Bag input, Func<int, bool> func)
            => null;
    }
}
