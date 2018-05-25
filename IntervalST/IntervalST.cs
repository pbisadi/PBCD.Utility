/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBCD.Algorithms.SearchTree;

namespace PBCD.Algorithms.ComputationalGeometry
{
    /// <summary>
    /// TODO: Watch Algorithm 11-4 and complete this
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class IntervalST<Key, Value> :RedBlackTree<Key, Value>
        where Key : IComparable
        where Value : class
    {
        protected class IntervalNode : RedBlackTree<Key, Value>.Node
        {
            public IntervalNode(Key k, Value v) : base(k, v)
            {

            }
            public Key Max;

        }

        /// <summary>
        /// Create interval search tree
        /// </summary>
        public IntervalST()
        {
            Stack<int> s = new Stack<int>(4);
        }

        public Value this[Key lo, Key hi]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(Key lo, Key hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Value> Intersects(Key lo, Key hi)
        {
            throw new NotImplementedException();
        }
    }
}
*/