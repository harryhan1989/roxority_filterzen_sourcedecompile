using System;
using System.Collections;

namespace DsLib
{
    public class BinaryTreeNode<T> where T : IComparable<T>
    {
        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Parent { get; set; }

        public int Height { get; protected internal set; }
    }
}
