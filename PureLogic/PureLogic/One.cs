namespace PureLogic
{
    public sealed class One: Reduce<Void>
    {
        private One() { }

        public static readonly One Value = new One();

        public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    }

    public static class OneX
    {
        static void A()
        {
            var x = from r in One where true select 7;
        }
    }
}
