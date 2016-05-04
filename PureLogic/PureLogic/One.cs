namespace PureLogic
{
    public static class OneX
    {
        static void A()
        {
            var x = from r in Bag where true select 7;
        }
    }
}
