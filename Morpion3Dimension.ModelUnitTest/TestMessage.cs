using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morpion3Dimension.Model;

namespace Morpion3Dimension.ModelUnitTest
{
    [TestClass]
    public class TestMessage
    {

        [TestMethod]
        public void  TestGetType()    
        {
            string data = "MOVE|1|2|3";
            var byteData = Encoding.UTF8.GetBytes(data);
            Assert.IsTrue(Message.GetMessageType(byteData) == MessageType.move);

        }

        [TestMethod]
        public void TestGetData()
        {
            string data = "MOVE|1|2|3";
            var byteData = Encoding.UTF8.GetBytes(data);
            var strData = Message.GetData(byteData);
            Assert.IsTrue(strData == "1|2|3");
        }

        [TestMethod]
        public void TestMoveFromString()
        {
            string data = "MOVE|1|2|3";
            var byteData = Encoding.UTF8.GetBytes(data);
            Move move = new Move(byteData);
            Assert.IsTrue(move.type == MessageType.move);
        }

        [TestMethod]
        public void TestMoveDataToString()
        {
            int[] coord = new int[] { 1, 2, 3 };
            var move = new Move(coord);
            var stri = move.DataToString();
            Assert.IsTrue("1|2|3" == stri);

        }

        [TestMethod]
        public void TestMoveToString()
        {
            int[] coord = new int[] { 1, 2, 3 };
            var move = new Move(coord);
            var stri = move.MessageToString();
            Assert.AreEqual("MOVE|1|2|3", stri );
        }

        [TestMethod]
        public void TestMove()
        {
            int[] coord = new int[] { 1, 2, 3 };
            var move = new Move(coord);
            var bytes = Encoding.UTF8.GetBytes(move.MessageToString());
            var move2 = new Move(bytes);
            Assert.AreEqual(move.MessageToString(), move2.MessageToString());


        }

        [TestMethod]
        public void TestGameOver()
        {
            var gameOv = new GameOverMessage(true);
            byte[] bytes = Encoding.UTF8.GetBytes(gameOv.MessageToString());
            var gameOv2 = new GameOverMessage(bytes);
            Assert.AreEqual(gameOv.MessageToString(), gameOv2.MessageToString());
        }

        [TestMethod]
        public void TestGameOverToString()
        {
            var gameOv = new GameOverMessage(true);
            Assert.AreEqual($"OVER|{true.ToString()}", gameOv.MessageToString());
        }

        [TestMethod]
        public void TestGrid()
        {
            var grid = new Grid();
            byte[] bytes = Encoding.UTF8.GetBytes(grid.MessageToString());
            var grid2 = new Grid(bytes);
            Assert.AreEqual(grid.MessageToString(), grid2.MessageToString());
        }

        [TestMethod]
        public void TestGridToString()
        {
            var grid = new Grid();
            string gridStr = grid.ToString();
            string res1= "0|0|0|\n";
            string res = "";
            for (int i = 0; i < 9; i++)
                res += res1;
            Assert.AreEqual(gridStr, res);
        }





    }
}
