using System;

namespace PureLogic
{
    public sealed class GroupBy<K, T> : Reduce<Tuple<K, T>>
    {
        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
