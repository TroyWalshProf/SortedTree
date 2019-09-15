using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using TroyWalshProf;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Order()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1, 3).Set("ing");
            thing.Visit(1, 1).Set("Pa");
            thing.Visit(1, 2).Set("ss");

            var singleNode = thing.Next();
            StringBuilder nodeValues = new StringBuilder();

            while (singleNode != null)
            {
                nodeValues.Append(singleNode.Value);
                singleNode = singleNode.Next();
            }

            Assert.AreEqual("Passing", nodeValues.ToString());
        }

        [TestMethod]
        public void OrderOffNode()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1, 9).Set("ing");
            thing.Visit(1, 3).Set("Pa");
            thing.Visit(1, 6).Set("ss");
            thing.Visit(1, 0).Set("NOT");

            var singleNode = thing.Visit(1, 0).Next();
            StringBuilder nodeValues = new StringBuilder();

            while (singleNode != null)
            {
                nodeValues.Append(singleNode.Value);
                singleNode = singleNode.Next();
            }

            Assert.AreEqual("Passing", nodeValues.ToString());
        }

        [TestMethod]
        public void MoveRoot()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1).Set("Node");

            Assert.ThrowsException<CircularStructureException>(() => thing.MoveTo(thing.Visit(1)));

        }

        [TestMethod]
        public void MoveBad()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1).Set("1");
            thing.Visit(1, 2).Set("12");
            thing.Visit(1, 2, 3).Set("123");

            Assert.ThrowsException<CircularStructureException>(() => thing.Visit(1).MoveTo(thing.Visit(1, 2, 3)));
        }

        [TestMethod]
        public void OrderWithMove()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1, 1).Set("Pa");
            thing.Visit(1, 3).Set("ing");
            thing.Visit(2).Set("ss");

            thing.Visit(2).MoveTo(thing.Visit(1));
            var singleNode = thing.Next();
            StringBuilder nodeValues = new StringBuilder();

            while (singleNode != null)
            {
                nodeValues.Append(singleNode.Value);
                singleNode = singleNode.Next();
            }

            Assert.AreEqual("Passing", nodeValues.ToString());
        }

        [TestMethod]
        public void OrderWithMoveWithReplace()
        {
            var thing = new SortedTree<int, String>();

            thing.Visit(1, 1).Set("Pa");
            thing.Visit(1, 3).Set("ing");
            thing.Visit(1, 2).Set("REPLACENODE");
            thing.Visit(1, 2, 3).Set("REPLACECHILD");
            thing.Visit(2).Set("ss");

            thing.Visit(2).MoveTo(thing.Visit(1), true);
            var singleNode = thing.Next();
            StringBuilder nodeValues = new StringBuilder();

            while (singleNode != null)
            {
                nodeValues.Append(singleNode.Value);
                singleNode = singleNode.Next();
            }

            Assert.AreEqual("Passing", nodeValues.ToString());
        }
    }
}
