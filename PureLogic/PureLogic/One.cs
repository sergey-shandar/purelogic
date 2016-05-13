using System.Linq;

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
        public static Bag<T> ToBag<T>(this T value)
            => One.Value.Select(_ => value);

        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb341042(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Bag<T> Empty<T>()
            => One.Value.SelectMany(_ => Enumerable.Empty<T>());
    }
}
