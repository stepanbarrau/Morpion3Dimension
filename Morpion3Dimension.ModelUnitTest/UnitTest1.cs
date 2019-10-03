using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morpion3Dimension.Model;

namespace Morpion3Dimension.ModelUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Game game = new Game(new RandomPlayer(Symbol.circle), new RandomPlayer(Symbol.cross));
            Assert.IsTrue(game.isOver);
            
        }
    }


}
