using System;

namespace PureLogic
{
    public sealed class DisjointUnion<T>: Map<T>
    {
        public Bag<T> InputA { get; }

        public Bag<T> InputB { get; }

        public DisjointUnion(Bag<T> inputA, Bag<T> inputB)
        {
            InputA = inputA;
            InputB = inputB;
        }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }

    public static class DisjointUnionX
    {
        /// <summary>
        /// See also https://msdn.microsoft.com/en-us/library/bb302894(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bag<T> DisjointUnion<T>(this Bag<T> a, Bag<T> b)
            => new DisjointUnion<T>(a, b);
    }
}
