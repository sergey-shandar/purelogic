﻿using System;

namespace PureLogic
{
    public sealed class Product<A, B> : Reduce<Tuple<A, B>>
    {
        public Bag<A> InputA { get; }

        public Bag<B> InputB { get; }

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

        public Product(Bag<A> a, Bag<B> b)
        {
            InputA = a;
            InputB = b;
        }
    }
}
