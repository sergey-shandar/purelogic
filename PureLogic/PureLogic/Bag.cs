using System;

namespace PureLogic
{
    /*
    public abstract class Bag
    {
        public interface IVisitor<R>
        {
            R Visit<T>(Bag<T> bag);
        }

        public abstract R Accept<R>(IVisitor<R> visitor);
    }
    */

    public abstract class Bag<T>//: Bag
    {
        //public override R Accept<R>(Bag.IVisitor<R> visitor) => visitor.Visit(this);

        public interface IVisitor<R>
        {
            R Visit(Map<T> map);
            R Visit(Reduce<T> reduce);
        }

        public abstract R Accept<R>(IVisitor<R> visitor);
    }
}
