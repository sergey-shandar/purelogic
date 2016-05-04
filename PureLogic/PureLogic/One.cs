namespace PureLogic
{
    public abstract class One: Bag<int>
    {
    }

    public static class OneX
    {
        static void A()
        {
            var x = from r in One where true select 7;
        }
    }
}
