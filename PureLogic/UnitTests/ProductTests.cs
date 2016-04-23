using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLogic;

namespace UnitTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductTest()
        {
            One.Value.Product(One.Value);
        }
    }
}
