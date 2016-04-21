namespace PureLogic
{
    public abstract class Reduce<T> : Bag<T>
    {
        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
