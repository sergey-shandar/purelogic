namespace PureLogic
{
    public abstract class Reduce<T> : Bag<T>
    {
        public override R Accept<R>(Bag<T>.IVisitor<R> visitor) => visitor.Visit(this);

        public new interface IVisitor<R>
        {
            R Visit(One one);
            R Visit(Input<T> input);
            R Visit<K, V>(GroupBy<K, V> groupBy);
            R Visit<A, B>(Product<A, B> product);
        }

        public abstract R Accept<R>(IVisitor<R> visitor);
    }
}
