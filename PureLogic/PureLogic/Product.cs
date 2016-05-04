using System;

namespace PureLogic
{
    public abstract class Product<A, B> : Reduce<Tuple<A, B>>
    {
    }
}
