using System;
using System.Collections.Generic;
using System.Collections;

namespace DsLib
{
    public class BinarySearchTree<T> : ICollection<T> where T : IComparable<T>
    {
        #region PropertiesAndFields

        private bool _isRefType = false;
        private int _count = 0;
        private GenericCompare1<T> _comparer = null;

        public delegate void CollectionModified(object sender, EventArgs e);

        protected event CollectionModified OnCollectionModified;

        public int Version { get; protected set; }

        public BinaryTreeNode<T> Root { get; set; }

        public BinaryTreeNode<T> StartNode { get; set; }

        public int Height { get; protected set; }

        #endregion

        #region Constructors

        public BinarySearchTree(IComparer<T> comparer) : this()
        {
            _comparer = new GenericCompare1<T>(comparer);
        }

        public BinarySearchTree()
        {
            if (typeof (T) == typeof (object))
            {
                _isRefType = true;
            }

            _comparer = new GenericCompare1<T>();

            Height = 0;

            Version = 1;
        }

        #endregion

        #region CollectionModfied

        public void SubscribeToModificationNotification(CollectionModified handler)
        {
            OnCollectionModified += handler;
        }

        public void UnSubscribeFromModificationNotification(CollectionModified handler)
        {
            if (OnCollectionModified != null)
                OnCollectionModified -= handler;
        }

        public void OnModify()
        {
            Version++;

            if (OnCollectionModified != null)
            {
                OnCollectionModified(this, null);
            }
        }

        #endregion

        #region Indexer

        // Though on BST indexer doesnt make sense.
        // Do BFS

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new InvalidOperationException("Index Out of Bounds");

                return TraverseToItem(index);
            }
        }

        private T TraverseToItem(int index)
        {
            Queue<BinaryTreeNode<T>> nodeQ = new Queue<BinaryTreeNode<T>>(2*index);

            int count = -1;
            nodeQ.Enqueue(Root);

            BinaryTreeNode<T> node = null;

            while (count != index)
            {
                node = nodeQ.Dequeue();
                count++;
                if (node.Left != null)
                {
                    nodeQ.Enqueue(node.Left);
                }
                if (node.Right != null)
                {
                    nodeQ.Enqueue(node.Right);
                }
            }

            return node.Value;
        }

        #endregion

        #region Add

        public void Add(T toAdd)
        {
            if (_isRefType && toAdd == null)
                return;

            AddToRoot(new BinaryTreeNode<T>(toAdd), true);
        }

        public void Add(T toAdd, bool isCompare)
        {
            if (_isRefType && toAdd == null)
                return;

            AddToRoot(new BinaryTreeNode<T>(toAdd), isCompare);
        }

        private void AddToRoot(BinaryTreeNode<T> node, bool isCompare)
        {
            if (node == null)
                return;

            if (Root == null)
            {
                Root = node;
                StartNode = node;
            }
            else if (isCompare)
            {
                AddNode(node, StartNode);
            }
            else
            {
                AddNodeWithoutCompare(node, StartNode);
            }


            Count++;

            OnModify();
        }

        private void AddNodeWithoutCompare(BinaryTreeNode<T> toAdd, BinaryTreeNode<T> where)
        {
            if (where == null)
                return;

            if (where.Left == null)
            {
                where.Left = toAdd;
                toAdd.Parent = where;
            }
            else
            {
                where.Right = toAdd;
                toAdd.Parent = where;
                StartNode = where.Left;
                //AddNode(toAdd, where.Left);
            }
        }

        private void AddNode(BinaryTreeNode<T> toAdd, BinaryTreeNode<T> where)
        {
            if (where == null)
                return;

            while (where != null)
            {
                int result = _comparer.Compare(toAdd.Value, where.Value);

                if (result < 0)
                {
                    if (where.Left == null)
                    {
                        where.Left = toAdd;
                        toAdd.Parent = where;
                        break;
                    }
                    else
                    {
                        where = where.Left;
                        continue;
                        //AddNode(toAdd, where.Left);
                    }
                }
                else
                {
                    if (where.Right == null)
                    {
                        where.Right = toAdd;
                        toAdd.Parent = where;
                        break;
                    }
                    else
                    {
                        where = where.Right;
                        continue;
                        //AddNode(toAdd, where.Right);
                    }
                }
            }
        }
        #endregion

        #region Contains
        public bool Contains(T toSearch)
        {
            BinaryTreeNode<T> t = SearchNode(new BinaryTreeNode<T>(toSearch), Root, false);

            return t != null ? true : false;
        }

        public BinaryTreeNode<T> SearchValue(T toSearch, BinaryTreeNode<T> where)
        {
            return SearchNode(new BinaryTreeNode<T>(toSearch), Root, false);
        }

        public BinaryTreeNode<T> SearchNode(BinaryTreeNode<T> toSearch, BinaryTreeNode<T> where, bool searchSameNode)
        {
            if (where == null)
                return null;

            while (where != null)
            {
                if (searchSameNode && toSearch == where)
                    break;

                else if (!searchSameNode && _comparer.IsEqual(toSearch.Value, where.Value))
                    break;

                else if (_comparer.IsLessThan(toSearch.Value, where.Value))
                {
                    where = where.Left;
                    continue;
                    //return SearchNode(toSearch, where.Left, searchSameNode);
                }
                else
                {
                    where = where.Right;
                    continue;
                    //return SearchNode(toSearch, where.Right, searchSameNode);
                }
            }

            return where;
        }

        public void SearchNodeInorder(BinaryTreeNode<T> toSearch, BinaryTreeNode<T> where, ref BinaryTreeNode<T> result)
        {
            if (where == null || result.Value.ToString() != "-1")
                return;
            SearchNodeInorder(toSearch, where.Left, ref result);
            if (_comparer.IsEqual(toSearch.Value, where.Value))
                result = where;
            SearchNodeInorder(toSearch, where.Right, ref result);
        }

        #endregion

        #region Remove
        public bool Remove(T toRemove)
        {
            BinaryTreeNode<T> foundNode = null;
            if (_isRefType && toRemove == null)
                return false;

            foundNode = SearchValue(toRemove, Root);

            if (foundNode != null)
            {
                RemoveNode(foundNode, true);
                OnModify();
                return true;
            }
            else
                return false;
        }

        private void RemoveNode(BinaryTreeNode<T> which, bool delSameNode)
        {
            if (which == null)
                return;

            Count--;

            BinaryTreeNode<T> parent = which.Parent;

            if (parent == null)
            {
                if (which.Right == null)
                {
                    Root = which.Left;
                }
                else if (which.Left == null)
                {
                    Root = which.Right;
                }
                else
                {
                    Root = which.Right;
                    BinaryTreeNode<T> leftmost = GetLeftMostNode(which.Right);
                    leftmost.Left = which.Left;
                    which.Left.Parent = leftmost;
                }

                if (Root != null)
                    Root.Parent = null;
            }
            else
            {
                bool isLeft = parent.Left == which;

                if (which.Right == null)
                {
                    if (isLeft)
                        parent.Left = which.Left;
                    else
                        parent.Right = which.Left;

                    if (which.Left != null)
                        which.Left.Parent = parent;
                }
                else
                {
                    if (isLeft)
                        parent.Left = which.Right;
                    else
                        parent.Right = which.Right;

                    which.Right.Parent = parent;

                    if (which.Left != null)
                    {
                        BinaryTreeNode<T> leftmost = GetLeftMostNode(which.Right);
                        leftmost.Left = which.Left;
                        which.Left.Parent = leftmost;
                    }
                }
            }

        }
        #endregion

        #region ICollection<T>
        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public int Count { get { return _count; } protected set { _count = value; } }

        public bool IsReadOnly { get { return true; } }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(array == null)
                throw new ArgumentNullException("array is null");

            if(arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex: " + arrayIndex + " is out of bounds");

            if (Count > array.Length || array.Rank > 1 || (arrayIndex + Count > array.Length))
                throw new ArgumentException("Array too small or is multidimensional");


            IEnumerator<T> e = GetEnumerator();

            while (e.MoveNext())
            {
                array[arrayIndex++] = e.Current;
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            BSTEnumerator<T> enumerator = new BSTEnumerator<T>(this, TreeTraversalType.InOrder);

            return enumerator;
        }

        public IEnumerator<T> GetEnumerator(TreeTraversalType type)
        {
            BSTEnumerator<T> enumerator = new BSTEnumerator<T>(this, type);

            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Traversal
        public string GetTraversePath(TreeTraversalType type)
        {
            string traversal = null;
            Traverse(type, out traversal);

            return traversal;
        }

        public void TraverseAndPrint(TreeTraversalType type)
        {
            string traversal = null;
            Traverse(type, out traversal);
        }

        public void Traverse(TreeTraversalType type, out string traversal)
        {
            traversal = type.ToString() + ":\n";

            switch (type)
            {
                case TreeTraversalType.PreOrder:
                    Preorder(StartNode, ref traversal);
                    break;
                case TreeTraversalType.InOrder:
                    Inorder(StartNode, ref traversal);
                    break;
                case TreeTraversalType.PostOrder:
                    Postorder(StartNode, ref traversal);
                    break;
                case TreeTraversalType.Layerorder:
                    Layerorder(StartNode, ref traversal);
                    break;
            }
        }

        private void Preorder(BinaryTreeNode<T> node, ref string traversal)
        {
            if (node == null)
                return;

            traversal += node.Value + "\n";

            Preorder(node.Left, ref traversal);
            Preorder(node.Right, ref traversal);
        }

        private void Inorder(BinaryTreeNode<T> node, ref string traversal)
        {
            if (node == null)
                return;

            Inorder(node.Left, ref traversal);

            traversal += node.Value + "\n";

            Inorder(node.Right, ref traversal);
        }

        private void Postorder(BinaryTreeNode<T> node, ref string traversal)
        {
            if (node == null)
                return;

            Postorder(node.Right, ref traversal);

            traversal += node.Value + "\n";

            Postorder(node.Left, ref traversal);
        }

        private void Layerorder(BinaryTreeNode<T> root, ref string traversal)
        {
            if (root == null) return;
            BinaryTreeNode<T> tmp = null;
            Queue queue = new Queue();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                tmp = (BinaryTreeNode<T>)queue.Dequeue();
                traversal += tmp.Value + "\n";
                if (tmp.Left != null)
                    queue.Enqueue(tmp.Left);
                if (tmp.Right != null)
                    queue.Enqueue(tmp.Right);
            }
        }
        #endregion

        #region Utils
        private BinaryTreeNode<T> GetLeftMostNode(BinaryTreeNode<T> where)
        {
            if (where == null)
                return null;

            //if (where.Left == null)
            //    return where;

            while (where.Left != null)
            {
                where = where.Left;
            }

            return where;
            //return GetLeftMostNode(where.Left);
        }
        
        public static bool ValueStatusInBST(BinarySearchTree<T> tree, T value)
        {
            if (tree == null)
            {
                return false;
            }

            return tree.Contains(value);
        }

        public bool PrintValueStatusInBST(T value)
        {
            return Contains(value);
        }

        public string CamlGenerate(BinaryTreeNode<T> root)
        {
            if (root == null) return null;
            if (root.Value.ToString() == "And" || root.Value.ToString() == "Or")
            {
                return string.Format("<" + root.Value.ToString() + ">{0}{1}" + "</" + root.Value.ToString() + ">", CamlGenerate(root.Left), CamlGenerate(root.Right));
            }
            else
            {
                return "("+root.Value.ToString()+")";
            }
        }
        #endregion

    }
}
