# SortedTree
An automatically sorted generic tree structure inspired by the MUMPS array structure.

# Usage

```C#
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
```

