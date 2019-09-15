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
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1, 3).Set("ing");
            sortedTree.Visit(1, 1).Set("Pa");
            sortedTree.Visit(1, 2).Set("ss");

            var singleNode = sortedTree.Next();
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
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1, 9).Set("ing");
            sortedTree.Visit(1, 3).Set("Pa");
            sortedTree.Visit(1, 6).Set("ss");
            sortedTree.Visit(1, 0).Set("NOT");

            var singleNode = sortedTree.Visit(1, 0).Next();
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
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1).Set("Node");

            Assert.ThrowsException<CircularStructureException>(() => sortedTree.MoveTo(sortedTree.Visit(1)));

        }

        [TestMethod]
        public void MoveBad()
        {
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1).Set("1");
            sortedTree.Visit(1, 2).Set("12");
            sortedTree.Visit(1, 2, 3).Set("123");

            Assert.ThrowsException<CircularStructureException>(() => sortedTree.Visit(1).MoveTo(sortedTree.Visit(1, 2, 3)));
        }

        [TestMethod]
        public void OrderWithMove()
        {
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1, 1).Set("Pa");
            sortedTree.Visit(1, 3).Set("ing");
            sortedTree.Visit(2).Set("ss");

            sortedTree.Visit(2).MoveTo(sortedTree.Visit(1));
            var singleNode = sortedTree.Next();
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
            var sortedTree = new SortedTree<int, String>();

            sortedTree.Visit(1, 1).Set("Pa");
            sortedTree.Visit(1, 3).Set("ing");
            sortedTree.Visit(1, 2).Set("REPLACENODE");
            sortedTree.Visit(1, 2, 3).Set("REPLACECHILD");
            sortedTree.Visit(2).Set("ss");

            sortedTree.Visit(2).MoveTo(sortedTree.Visit(1), true);
            var singleNode = sortedTree.Next();
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
