using System;

namespace PureLogic
{
    public sealed class DisjointUnion<T>: Map<T>
    {
        public readonly Bag<T> InputA;

        public readonly Bag<T> InputB;

        public DisjointUnion(Bag<T> inputA, Bag<T> inputB)
        {
            InputA = inputA;
            InputB = inputB;
        }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
