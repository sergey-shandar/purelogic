namespace PureLogic
{
    public abstract class One: Reduce<Void>
    {
        private One() { }
    }

    public static class OneX
    {
        static void A()
        {
            var x = from r in One where true select 7;
        }
    }
}
