using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLogic;
using FluentAssertions;

namespace UnitTests
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void WhereSelectTest()
        {
            var r = from x in 3.Option() where x == 3 select x + 4;
            r.HasValue.Should().BeTrue();
            r.Value.Should().Be(7);
        }
    }
}
