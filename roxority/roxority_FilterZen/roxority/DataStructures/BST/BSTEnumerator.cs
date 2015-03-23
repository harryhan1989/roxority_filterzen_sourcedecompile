using System;
using System.Collections.Generic;
using System.Collections;

namespace DsLib
{
    public class BSTEnumerator<T> : IEnumerator<T>  where T : IComparable<T>
    {
        BinarySearchTree<T> _tree = null;
        int _cursor = -1;
        int _arrIndex = 0;
        T[] _array = null;
        int _version = 0;
        bool _invalidated = false;

        public BSTEnumerator(BinarySearchTree<T> bst, TreeTraversalType type)
        {
            _tree = bst;
            if (_tree.Count > 0)
            {
                _arrIndex = 0;
                _array = new T[_tree.Count];
                BuildIterableCollection(type);
                _version = _tree.Version;
                _tree.SubscribeToModificationNotification(OnTreeChangeHandler);
            }
        }

        public void OnTreeChangeHandler(object sender, EventArgs e)
        {
            _invalidated = true;
        }

        // Summary:
        //     Performs application-defined tasks associated with freeing, releasing, or
        //     resetting unmanaged resources.
        public void Dispose()
        {
            ((IEnumerator)this).Reset();
            _arrIndex = 0;
            _array = null;
        }

        // Summary:
        //     Gets the element in the collection at the current position of the enumerator.
        //
        // Returns:
        //     The element in the collection at the current position of the enumerator.
        T IEnumerator<T>.Current 
        { 
            get 
            {
                if (_version != _tree.Version || _invalidated)
                    throw new InvalidOperationException("Collection modified: Iterator is invalidated");

                if (_cursor < 0 || _cursor >= _array.Length)
                    throw new InvalidOperationException("Iterator position invalid");
                else 
                    return _array[_cursor];
            } 
        }

        // Summary:
        //     Gets the current element in the collection.
        //
        // Returns:
        //     The current element in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The enumerator is positioned before the first element of the collection or
        //     after the last element.
        object IEnumerator.Current
        {
            get
            {
                return ((IEnumerator<T>)this).Current;
            }
        }

        // Summary:
        //     Advances the enumerator to the next element of the collection.
        //
        // Returns:
        //     true if the enumerator was successfully advanced to the next element; false
        //     if the enumerator has passed the end of the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        bool IEnumerator.MoveNext()
        {

            if (_version != _tree.Version || _invalidated)
                throw new InvalidOperationException("Collection modified: Iterator is invalidated");

            _cursor++;

            if (_cursor >= 0 && _cursor < _array.Length)
                return true;
            else
                return false;
        }
        
        //
        // Summary:
        //     Sets the enumerator to its initial position, which is before the first element
        //     in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        void IEnumerator.Reset()
        {
            if (_version != _tree.Version || _invalidated)
                throw new InvalidOperationException("Collection modified: Iterator is invalidated");

            _cursor = -1;
        }

        private void BuildIterableCollection(TreeTraversalType type)
        {
            switch (type)
            {
                case TreeTraversalType.PreOrder:
                    Preorder(_tree.Root);
                    break;
                case TreeTraversalType.InOrder:
                    Inorder(_tree.Root);
                    break;
                case TreeTraversalType.PostOrder:
                    Postorder(_tree.Root);
                    break;
            }
        }

        private void Preorder(BinaryTreeNode<T> node)
        {
            if (node == null)
                return;

            _array[_arrIndex++] = node.Value;

            Preorder(node.Left);
            Preorder(node.Right);
        }

        private void Inorder(BinaryTreeNode<T> node)
        {
            if (node == null)
                return;

            Inorder(node.Left);

            _array[_arrIndex++] = node.Value;

            Inorder(node.Right);
        }

        private void Postorder(BinaryTreeNode<T> node)
        {
            if (node == null)
                return;

            Postorder(node.Right);

            _array[_arrIndex++] = node.Value;

            Postorder(node.Left);
        }
    }
}
