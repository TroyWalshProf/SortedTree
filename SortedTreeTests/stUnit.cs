using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using TroyWalshProf;

namespace MumpTests
{
    [TestClass]
    public class stUnit
    {
        [TestMethod]
        public void TestMethod1()
        {
            SortedTree<int, string> thing = new SortedTree<int, string>();
            var node = thing.Set("dfdf", 1, 2, 3, 4, 5);
            node = thing.Set("d", 1, 2);
            node = thing.Set("zz", 2, 1);
        }

        [TestMethod]
        public void TestM2()
        {
            var tree = new SortedTree<int, String>();
            tree.Set("12345b", 1, 2, 3, 4, 5);
            tree.Visit(1, 2, 3, 4, 6).Set("12346b");
            tree.Visit(2, 2).Set("22e");
            tree.Visit(3, 2).Set("32f");
            var singleNode = tree.Next();

            while (singleNode != null)
            {
                Console.WriteLine(singleNode.Value + "    " + string.Join(", ", singleNode.GetPath()));
                singleNode = singleNode.Next();
            }

        }

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
