using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.SearchTree
{
    /// <summary>
    /// It is a Left Leaning Red-Black tree (LLRB Tree)
    /// </summary>
    public class RedBlackTree<TKey, TValue>
        where TKey : IComparable
        where TValue : class
    {
        protected class Node
        {
            public TKey key;
            public TValue value;
            public Node left, right;
            public int Count;   //Number of nodes in the tree rooted by this node
            public Node(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                this.Color = RED;   //New nodes are always red
                Count = 1;
            }
            public bool Color;
        }

        Node root;
        private static readonly bool RED = true;
        private static readonly bool BLACK = false;

        public TValue this[TKey key]
        {
            get
            {
                return Get(root, key).value;
            }
            set
            {
                root = Put(root, key, value);
                root.Color = BLACK;
            }
        }

        private Node Get(Node x, TKey key)
        {
            while (x != null)
            {
                int cmp = key.CompareTo(x.key);
                if (cmp < 0) x = x.left;
                else if (cmp > 0) x = x.right;
                else return x;
            }
            return null;
        }

        private Node Put(Node x, TKey key, TValue value)
        {
            if (x == null) return new Node(key, value);

            if (IsRed(x.left) && IsRed(x.right)) FlipColors(x);

            int cmp = key.CompareTo(x.key);
            if (cmp == 0) x.value = value;
            else if (cmp < 0) x.left = Put(x.left, key, value);
            else x.right = Put(x.right, key, value);

            if (IsRed(x.right) && !IsRed(x.left)) x = RotateLeft(x);
            if (IsRed(x.left) && IsRed(x.left.left)) x = RotateRight(x);

            return SetCount(x);
        }

        private Node SetCount(Node x)
        {
            x.Count = 1 + Size(x.left) + Size(x.right);
            return x;
        }

        private Node RotateLeft(Node h)
        {
            Debug.Assert(IsRed(h.right));
            Node x = h.right;
            h.right = x.left;
            x.left = SetCount(h);
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }

        private Node RotateRight(Node h)
        {
            Debug.Assert(IsRed(h.left));
            Node x = h.left;
            h.left = x.right;
            x.right = SetCount(h);
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }

        private void FlipColors(Node h)
        {
            h.Color = !h.Color;
            h.left.Color = !h.left.Color;
            h.right.Color = !h.right.Color;
        }

        public IEnumerable<TKey> Enumerator()
        {
            Queue<TKey> q = new Queue<TKey>();
            InOrder(root, q);
            return q;
        }

        private void InOrder(Node x, Queue<TKey> q)
        {
            if (x == null) return;
            InOrder(x.left, q);
            q.Enqueue(x.key);
            InOrder(x.right, q);
        }

        public Tuple<TKey, TValue> Floor(TKey key)
        {
            Node x = Floor(root, key);
            if (x == null) return null;
            return new Tuple<TKey, TValue>(x.key, x.value);
        }

        private Node Floor(Node x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);

            if (cmp == 0) return x;

            if (cmp < 0) return Floor(x.left, key);

            Node t = Floor(x.right, key);
            if (t != null) return t; //If there is a key larger than x and less than key
            else return x; //Then x is the largest node lest than key            
        }

        public int Size() { return Size(root); }

        private int Size(Node x)
        {
            if (x == null) return 0;
            return x.Count;
        }

        public Tuple<TKey, TValue> Min()
        {
            var x = Min(root);
            return new Tuple<TKey, TValue>(x.key, x.value);
        }

        private Node Min(Node x)
        {
            if (x == null) return null;
            while (x.left != null) x = x.left;
            return x;
        }

        public Tuple<TKey, TValue> Max()
        {
            var x = Max(root);
            return new Tuple<TKey, TValue>(x.key, x.value);
        }

        private Node Max(Node x)
        {
            if (x == null) return null;
            while (x.right != null) x = x.right;
            return x;
        }

        public int Rank(TKey key)
        { return Rank(key, root); }

        private int Rank(TKey key, Node x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Rank(key, x.left);
            else if (cmp > 0) return 1 + Size(x.left) + Rank(key, x.right);
            else return Size(x.left);
        }

        public void DeleteMin()
        {
            root = DeleteMin(root);
            root.Color = BLACK;
        }

        private Node DeleteMin(Node x)
        {
            if (x.left == null) return null;

            if (!IsRed(x.left) && !IsRed(x.left.left))
                x = MoveRedLeft(x);

            x.left = DeleteMin(x.left);

            x.Count = 1 + Size(x.left) + Size(x.right);
            return FixUp(x);
        }

        private Node FixUp(Node h)
        {
            if (IsRed(h.right))
                h = RotateLeft(h);

            if (IsRed(h.left) && IsRed(h.left.left))
                h = RotateRight(h);

            if (IsRed(h.left) && IsRed(h.right))
                FlipColors(h);

            h.Count = 1 + Size(h.left) + Size(h.right);
            return h;
        }

        public void DeleteMax()
        { root = DeleteMax(root); }

        private Node DeleteMax(Node x)
        {
            if (x.right == null) return x.left;
            x.right = DeleteMax(x.right);
            x.Count = 1 + Size(x.left) + Size(x.right);
            return x;
        }

        public void Delete(TKey key)
        {
            root = Delete(root, key);
            root.Color = BLACK;
        }

        /// <summary>
        /// Use Hibbard Deletion
        /// </summary>
        private Node Delete(Node h, TKey key)
        {
            if (h == null) return null;
            int cmp = key.CompareTo(h.key);
            if (cmp < 0)
            {
                if (!IsRed(h.left) && !IsRed(h.left.left))
                    h = MoveRedLeft(h);
                h.left = Delete(h.left, key);
            }
            else
            {
                if (IsRed(h.left))
                    h = RotateRight(h);
                if (key.CompareTo(h.key) == 0 && (h.right == null))
                    return null;
                if (!IsRed(h.right) && !IsRed(h.right.left))
                    h = MoveRedRight(h);
                if (key.CompareTo(h.key) == 0)
                {
                    var t = Get(h.right, Min(h.right).key);
                    h.value = t.value;
                    h.key = t.key;
                    h.right = DeleteMin(h.right);
                }
                else
                    h.right = Delete(h.right, key);
            }

            h.Count = 1 + Size(h.left) + Size(h.right);
            return FixUp(h);
        }

        private Node MoveRedRight(Node h)
        {
            // Assuming that h is red and both h.right and h.right.left
            // are black, make h.right or one of its children red.
            Debug.Assert(IsRed(h));
            Debug.Assert(!IsRed(h.right));
            Debug.Assert(!IsRed(h.right.left));

            FlipColors(h);
            if (IsRed(h.left.left))
            {
                h = RotateRight(h);
                FlipColors(h);
            }
            return h;
        }

        private Node MoveRedLeft(Node h)
        {
            // Assuming that h is red and both h.left and h.left.left
            // are black, make h.left or one of its children red.
            Debug.Assert(IsRed(h));
            Debug.Assert(!IsRed(h.left));
            Debug.Assert(!IsRed(h.left.right));

            FlipColors(h);
            if (IsRed(h.right.left))
            {
                h.right = RotateRight(h.right);
                h = RotateLeft(h);
                FlipColors(h);
            }
            return h;
        }

        private bool IsRed(Node x)
        {
            if (x == null) return false;
            return x.Color == RED;
        }


    }
}
