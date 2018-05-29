using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.SearchTree
{
    public class BinarySearchTree<TKey, TValue>
        where TKey : IComparable
        where TValue : class
    {
        class Node
        {
            public TKey key;
            public TValue value;
            public Node left, right;
            public int Count;   //Number of nodes in the tree rooted by this node
            public Node(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                Count = 1;
            }
        }

        Node root;

        public TValue this[TKey key]
        {
            get
            {
                Node x = root;
                while (x != null)
                {
                    int cmp = key.CompareTo(x.key);
                    if (cmp < 0) x = x.left;
                    else if (cmp > 0) x = x.right;
                    else return x.value;
                }
                return null;
            }
            set
            {
                root = Put(root, key, value);
            }
        }

        private Node Put(Node x, TKey key, TValue value)
        {
            if (x == null) return new Node(key, value);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0)
                x.left = Put(x.left, key, value);
            else if (cmp > 0)
                x.right = Put(x.right, key, value);
            else
                x.value = value;
            x.Count = 1 + Size(x.left) + Size(x.right);
            return x;
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
        { root = DeleteMin(root); }

        private Node DeleteMin(Node x)
        {
            if (x.left == null) return x.right;
            x.left = DeleteMin(x.left);
            x.Count = 1 + Size(x.left) + Size(x.right);
            return x;
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
        { root = Delete(root, key); }

        /// <summary>
        /// Use Hibbard Deletion
        /// </summary>
        private Node Delete(Node x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = Delete(x.left, key);
            else if (cmp > 0) x.right = Delete(x.right, key);
            else
            {
                if (x.right == null) return x.left;

                Node t = x;
                x = Min(t.right);
                x.right = DeleteMin(t.right);
                x.left = t.left;
            }
            x.Count = 1 + Size(x.left) + Size(x.right);
            return x;
        }
    }


}
