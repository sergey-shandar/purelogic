using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLogic;

namespace UnitTests
{
    [TestClass]
    public class SelectManyTests
    {
        [TestMethod]
        public void SelectManyTest()
        {
            var selectMany = One.Value
                .SelectMany(_ => "hello world!")
                .Select(c => "x" + c)
                .Where(s => s == "xh")
                .OfType<int>();
        }

        [TestMethod]
        public void LinqStyleTest()
        {
            var x = from r in One.Value where true select 7;
        }

    }
}
