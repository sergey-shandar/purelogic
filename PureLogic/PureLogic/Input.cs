namespace PureLogic
{
    public sealed class Input<T> : Reduce<T>
    {
        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }
}
