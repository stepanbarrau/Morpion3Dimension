using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morpion3Dimension.Model;

namespace Morpion3Dimension.ModelUnitTest
{
    [TestClass]
    public class UnitTest1
    {
       /* [TestMethod]
        public void TestMethod1()
        {
            Game game = new Game(new RandomPlayer(Symbol.circle), new RandomPlayer(Symbol.cross));
            Assert.IsTrue(game.isOver);
            
        }*/

        [TestMethod]
        public void TestCheckWin()
        {
            var grid = new Grid();
            Rules rules = new Rules();
            Move move = null;

            for (var i = 0; i < 3; i++)
            {
                int[] coord = { i, 0, 0 };
                move = new Move(coord);
                grid.PlayMove(move, Symbol.circle);
                
            }
            Assert.IsTrue(rules.winCheck(move, Symbol.circle, grid));

        }

        [TestMethod]
        public void TestCheckWinFalse()
        {
            var grid = new Grid();
            Rules rules = new Rules();
            int[] coord = { 0, 0, 0 };
            Move move = new Move(coord);
            grid.PlayMove(move, Symbol.circle);
            Assert.IsFalse(rules.winCheck(move, Symbol.circle, grid));
        }

        // Fill the grid with circle and check isDraw is equal to true
        [TestMethod]
        public void TestCheckDraw()
        {
            var grid = new Grid();
            var rules = new Rules();
            Assert.IsFalse(rules.IsDraw(grid));

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Square square = (Square)grid[i, j, k];
                        square.symbol = Symbol.circle;
                    }
                }
            }

            Assert.IsTrue(rules.IsDraw(grid));
        }

        [TestMethod]
        public void TestPlayMove()
        {
            var grid = new Grid();
            Rules rules = new Rules();
            int[] coord = { 0, 0, 0 };
            Move move = new Move(coord);
            grid.PlayMove(move, Symbol.circle);
            var square = (Square) grid[0, 0, 0];
            Assert.IsTrue(square.symbol == Symbol.circle);
        }
    }


}
