using System;

namespace PureLogic
{
    public sealed class One: Reduce<Void>
    {
        private One() { }

        public static readonly One Value = new One();

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }

    public static class OneX
    {
        public static Bag<T> Const<T>(this T value)
            => One.Value.Select(_ => value);
    }
}
