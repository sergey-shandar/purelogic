# PureLogic

## Bag Classes

See also [Multiset](https://en.wikipedia.org/wiki/Multiset).

- [Bag](PureLogic/PureLogic/Bag.cs)
    - [Map](PureLogic/PureLogic/Map.cs)
        - [SelectMany](PureLogic/PureLogic/SelectMany.cs)
        - [DisjointUnion](PureLogic/PureLogic/DisjointUnion.cs)
    - [Reduce](PureLogic/PureLogic/Reduce.cs)
        - [One](PureLogic/PureLogic/One.cs)
        - [Input](PureLogic/PureLogic/Input.cs)
        - [GroupBy](PureLogic/PureLogic/GroupBy.cs)
        - [Product](PureLogic/PureLogic/Product.cs)

## Methods

See [Enumerable Methods](https://msdn.microsoft.com/en-us/library/system.linq.enumerable_methods(v=vs.110).aspx).

- Aggregate
    - `Bag<Option<T>> Bag<T>.Aggregate<T>(Func<T, T, T> func)`
    - `Bag<T> Bag<T>.Aggregate<T>(T default, Func<T, T, T> func)`
    - All/Any/DefaultIfEmpty/Contains
        - `Bag<bool> Bag<T>.All(Func<T, bool> func)`
        - `Bag<bool> Bag<T>.Any(Func<T, bool> func)`
        - `Bag<bool> Bag<T>.Contains<T>(T value)`
        - `Bag<T> Bag<T>.DefaultIfEmpty(T value = default(T))`
    - Min/Max
        - `Bag<Option<T>> Bag<T>.Min()`
        - `Bag<Option<T>> Bag<T>.Max()`
    - Average/Count
        - Average
            - `Bag<long> Bag<long>.Average<long>()`
            - `Bag<double> Bag<double>.Average<double>()`
            - `Bag<decimal> Bag<decimal>.Average<decimal>()`
        - Count
            - `Bag<long> Bag<T>.Count()`
- Distinct/Except
    - `Bag<T> Bag<T>.Distinct(Bag<T> b)`
    - `Bag<T> Bag<T>.Except(Bag<T> b)`
- Emptry
    - `Bag<T> Empty<T>()`
- GroupBy
    - `Bag<KeyValuePair<K, V>> Bag<KeyValuePair<K, V>>.GroupBy(Func<V, V, V> func)`
    - `Bag<KeyValuePair<K, T>> Bag<T>.GroupBy(Func<T, K> keyFunc, Func<T, T, T> reduceFunc)`
     
## Utilities

- [Option](PureLogic/PureLogic/Option.cs)
- [NumericPolicy](PureLogic/PureLogic/NumericPolicy.cs)
