using System.Collections.Generic;

namespace PureLogic
{
    public static class KeyValuePairX
    {
        public static KeyValuePair<K, V> WithValue<K, V>(this K key, V value)
            => new KeyValuePair<K, V>(key, value);
    }
}
