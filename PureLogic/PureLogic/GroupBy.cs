using System;
using System.Collections.Generic;

namespace PureLogic
{
    public abstract class GroupBy<K, T> : Reduce<KeyValuePair<K, T>>
    {
    }
}
