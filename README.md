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
            - `Bag<long> Bag<long>.Average()`
            - `Bag<double> Bag<double>.Average()`
            - `Bag<decimal> Bag<decimal>.Average()`
        - Count
            - `Bag<long> Bag<T>.Count()`
        - Sum
            - `Bag<long> Bag<long>.Sum()` 
            - `Bag<double> Bag<double>.Sum()`
            - `Bag<decimal> Bag<decimal>.Sum()`
- Distinct/Except/BagEqual
    - `Bag<T> Bag<T>.Distinct(Bag<T> b)`
    - `Bag<T> Bag<T>.Except(Bag<T> b)`
    - `Bag<T> Bag<T>.Intersect(Bag<T> b)`
    - `Bag<bool> Bag<T>.Equal(Bag<T> b)`
    - `Bag<T> Bag<T>.Union(Bag<T> b)`
- Emptry
    - `Bag<T> Empty<T>()`
- GroupBy
    - `Bag<KeyValuePair<K, V>> Bag<KeyValuePair<K, V>>.GroupBy(Func<V, V, V> func)`
    - `Bag<KeyValuePair<K, T>> Bag<T>.GroupBy(Func<T, K> keyFunc, Func<T, T, T> reduceFunc)`
- Select
    - `Bag<T> Bag<S>.OfType<S, T>()`
    - `Bag<T> Bag<S>.Select<S, T>(Func<S, T> func)`
    - `Bag<T> Bag<S>.Select<S, T>(Func<S, IEnumerable<T>> func)`
    - `Bag<T> Bag<T>.Where<T>(Func<T, bool> func)`
     
## Utilities

- [Option](PureLogic/PureLogic/Option.cs)
- [NumericPolicy](PureLogic/PureLogic/NumericPolicy.cs)
