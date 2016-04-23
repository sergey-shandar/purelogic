using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLogic;

namespace UnitTests
{
    [TestClass]
    public class DisjointUnionTests
    {
        [TestMethod]
        public void DisjointUnionTest()
        {
            var twoValues = One.Value.DisjointUnion(One.Value);
        }
    }
}
