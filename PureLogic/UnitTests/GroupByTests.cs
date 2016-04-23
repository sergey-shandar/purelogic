using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLogic;
using System;

namespace UnitTests
{
    [TestClass]
    public class GroupByTests
    {
        [TestMethod]
        public void GroupByTest()
        {
            new Input<Tuple<int, string>>().GroupBy((a, b) => a + b);
        }
    }
}
