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
}
