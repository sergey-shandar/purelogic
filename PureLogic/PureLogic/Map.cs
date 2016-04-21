using System;

namespace PureLogic
{
    public abstract class Map<T> : Bag<T>
    {
        public override R Accept<R>(Bag<T>.IVisitor<R> visitor) => visitor.Visit(this);

        public new interface IVisitor<R>
        {
            R Visit<I>(SelectMany<I, T> selectMany);
            R Visit(DisjointUnion<T> disjointUnion);
        }

        public abstract R Accept<R>(IVisitor<R> visitor);
    }
}
