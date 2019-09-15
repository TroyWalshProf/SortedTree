using System;
using System.Collections.Generic;
using System.Linq;

namespace TroyWalshProf
{
    [Serializable]
    public class SortedTree<KeyType, ValueType>
    {
        public SortedTree()
        {
            this.IsRoot = true;
        }

        public SortedTree(ref KeyType parentKey, SortedTree<KeyType, ValueType> parentNode)
        {
            this.ParentKey = parentKey;
            this.ParentNode = parentNode;
            this.IsRoot = false;
        }

        public SortedTree<KeyType, ValueType> ParentNode { get; private set; }

        public KeyType ParentKey { get; private set; }

        public bool IsRoot { get; private set; } = false;

        public void MoveTo(SortedTree<KeyType, ValueType> node, bool replaceIfExists = false)
        {
            if (this.IsRoot)
            {
                throw new CircularStructureException("The root node can not be moved");
            }

            var parentNode = node;

            while (parentNode!= null)
            {

                if (parentNode.ChildIsSet(this.ParentKey))
                {
                    var value = parentNode.List[this.ParentKey];

                }

                if (this == parentNode)
                {
                    throw new CircularStructureException("cannot be assigned as your own ancestor");
                }

                parentNode = parentNode.ParentNode;
            } 


            if (node.List.ContainsKey(this.ParentKey))
            {
                if (replaceIfExists)
                {
                    node.List.Remove(this.ParentKey);
                }
                else
                {
                    throw new ArgumentException("Node alreay exists");
                }
            }

            node.List.Add(this.ParentKey, this);
            this.ParentNode.List.Remove(this.ParentKey);
            this.ParentNode = node;

        }

        public SortedList<KeyType, SortedTree<KeyType, ValueType>> List { get; set; } = new SortedList<KeyType, SortedTree<KeyType, ValueType>>();

        public ValueType Value { get; set; }

        public bool IsSet { get; set; } = false;

        public void Set(ValueType value)
        {
            Value = value;
            IsSet = true;
        }

        public void Clear()
        {
            //Value = null;
            IsSet = false;
        }

        public bool ChildIsSet(KeyType key)
        {
            return List.ContainsKey(key) ? List[key].IsSet : false;
        }

        public SortedTree<KeyType, ValueType> Visit(params KeyType[] keys)
        {
            if (keys.Any())
            {
                KeyType key = keys[0];
                keys = keys.Skip(1).ToArray();

                if (List.ContainsKey(key))
                {
                    return List[key].Visit(keys);
                }
                else
                {
                    var node = new SortedTree<KeyType, ValueType>(ref key, this);
                    List.Add(key, node);
                    return node.Visit(keys);
                }
            }
            else
            {
                return this;
            }
        }

        public SortedTree<KeyType, ValueType> Set(ValueType value, params KeyType[] keys)
        {
            var node = Visit(keys);
            node.Set(value);
            return node;
        }

        public SortedTree<KeyType, ValueType> Next()
        {
            return Next(this);
        }

        private static SortedTree<KeyType, ValueType> Next(SortedTree<KeyType, ValueType> thisNode)
        {
            // Check for childeren first
            var nextNode = FirstChild(thisNode);

            if (nextNode != null && nextNode.IsSet)
            {
                return nextNode;
            }

            // Looks for siblings instead
            nextNode = Siblings(thisNode);

            if (nextNode != null && nextNode.IsSet)
            {
                return nextNode;
            }

            return null;
        }

        private static SortedTree<KeyType, ValueType> FirstChild(SortedTree<KeyType, ValueType> thisNode)
        {
            // Check for childeren first
            foreach (KeyType key in thisNode.List.Keys)
            {

                var temp = thisNode.List[key];

                if (temp != null && temp.IsSet)
                {
                    return temp;
                }

                temp = FirstChild(temp);

                if (temp != null && !temp.IsRoot && temp.IsSet)
                {
                    return temp;
                }
            }

            return null;
        }


        private static SortedTree<KeyType, ValueType> Siblings(SortedTree<KeyType, ValueType> thisNode)
        {
            SortedTree<KeyType, ValueType> currentNode = thisNode;

            if (!currentNode.IsRoot)
            {
                int keyIndex = currentNode.ParentNode.List.IndexOfKey(currentNode.ParentKey) + 1;

                foreach (KeyType key in currentNode.ParentNode.List.Keys.Skip(keyIndex))
                {
                    var newTempNode = currentNode.ParentNode.List[key];

                    if (newTempNode != null)
                    {
                        if (!newTempNode.IsSet)
                        {
                            newTempNode = Next(newTempNode);
                        }

                        if (newTempNode != null && newTempNode.IsSet)
                        {
                            return newTempNode;
                        }
                    }
                }

                return Siblings(currentNode.ParentNode);

            }

            return null;
        }


        public KeyType[] GetPath()
        {
            SortedTree<KeyType, ValueType> currentNode = this;
            Stack<KeyType> keys = new Stack<KeyType>();

            while (!currentNode.IsRoot)
            {
                keys.Push(currentNode.ParentKey);
                currentNode = currentNode.ParentNode;
            }

            return keys.ToArray();
        }
    }
}
