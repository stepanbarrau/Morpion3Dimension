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
        public void TestMoveToBinary()
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

    }
}
