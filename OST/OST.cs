using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.SearchTree
{
    /// <summary>
    /// Order Statistics Tree
    /// is a RedBlack tree which is able to return the rank of each elements
    /// </summary>
    public class OrderStatisticTree<TKey, TValue>
    {
        // the number of nodes contained in the tree
        private int intCount;
        // a simple randomized hash code. The hash code could be used as a key
        // if it is "unique" enough. Note: The IComparable interface would need to 
        // be replaced with int.
        private int intHashCode;
        // identifies the owner of the tree
        private string strIdentifier;
        // the tree
        private RedBlackNode<TKey, TValue> rbTree;
        //  sentinelNode is convenient way of indicating a leaf node.
        private static RedBlackNode<TKey, TValue> sentinelNode;
        // the node that was last found; used to optimize searches
        private RedBlackNode<TKey, TValue> lastNodeFound;
        private Random rand = new Random();

        private IComparer<TKey> comparer;
        private int BLACK = RedBlackNode<TKey, TValue>.BLACK;
        private int RED = RedBlackNode<TKey, TValue>.RED;


        public OrderStatisticTree(IComparer<TKey> keyComparer)
        {
            strIdentifier = base.ToString() + rand.Next();
            intHashCode = rand.Next();

            // set up the sentinel node. the sentinel node is the key to a successfull
            // implementation and for understanding the red-black tree properties.
            sentinelNode = new RedBlackNode<TKey, TValue>();
            sentinelNode.Left = null;
            sentinelNode.Right = null;
            sentinelNode.Parent = null;
            sentinelNode.Color = RedBlackNode<TKey, TValue>.BLACK;
            rbTree = sentinelNode;
            lastNodeFound = sentinelNode;
            this.comparer = keyComparer;
        }

        public int IndexOf(TKey x)
        {
            return Rank(rbTree, x) - 1;
        }

        private int Rank(RedBlackNode<TKey, TValue> root, TKey x)
        {
            if (comparer.Compare(x, root.Key) == -1)
            {
                return Rank(root.Left, x);
            }
            else if (comparer.Compare(x, root.Key) == 0)
            {
                if (root.Left.Key != null)
                    return root.Left.Count + 1;
                else
                    return 1;
            }
            else if (comparer.Compare(x, root.Key) == 1)
            {
                if (root.Left.Key != null)
                {
                    return root.Left.Count + 1 + Rank(root.Right, x);
                }
                else
                {
                    return 1 + Rank(root.Right, x);
                }
            }
            return -1;
        }

        ///<summary>
        /// Add
        /// args: ByVal key As IComparable, ByVal data As Object
        /// key is object that implements IComparable interface
        /// performance tip: change to use use int type (such as the hashcode)
        ///</summary>
        public void Add(TKey key, TValue data)
        {
            if (key == null)
                throw (new Exception("RedBlackNode key must not be null"));

            // traverse tree - find where node belongs
            int result = 0;
            // create new node
            RedBlackNode<TKey, TValue> node = new RedBlackNode<TKey, TValue>();
            RedBlackNode<TKey, TValue> temp = rbTree;				// grab the rbTree node of the tree

            while (temp != sentinelNode)
            {	// find Parent
                node.Parent = temp;
                temp.Count++;   //The new node will be added to temp.Right or temp.Left anyway
                result = comparer.Compare(key, temp.Key);
                if (result == 0)
                    throw (new Exception("A Node with the same key already exists"));
                if (result > 0)
                    temp = temp.Right;
                else
                    temp = temp.Left;
            }

            // setup node
            node.Key = key;
            node.Data = data;
            node.Left = sentinelNode;
            node.Right = sentinelNode;
            node.Count = 1;

            // insert node into tree starting at parent's location
            if (node.Parent != null)
            {
                result = comparer.Compare(node.Key, node.Parent.Key);
                if (result > 0)
                    node.Parent.Right = node;
                else
                    node.Parent.Left = node;
            }
            else
                rbTree = node;					// first node added

            RestoreAfterInsert(node);           // restore red-black properities

            lastNodeFound = node;

            intCount = intCount + 1;
        }
        ///<summary>
        /// RestoreAfterInsert
        /// Additions to red-black trees usually destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it
        ///</summary>
        private void RestoreAfterInsert(RedBlackNode<TKey, TValue> x)
        {
            // x and y are used as variable names for brevity, in a more formal
            // implementation, you should probably change the names

            RedBlackNode<TKey, TValue> y;

            // maintain red-black tree properties after adding x
            while (x != rbTree && x.Parent.Color == RedBlackNode<TKey, TValue>.RED)
            {
                // Parent node is .Colored red; 
                if (x.Parent == x.Parent.Parent.Left)	// determine traversal path			
                {										// is it on the Left or Right subtree?
                    y = x.Parent.Parent.Right;			// get uncle
                    if (y != null && y.Color == RedBlackNode<TKey, TValue>.RED)
                    {	// uncle is red; change x's Parent and uncle to black
                        x.Parent.Color = RedBlackNode<TKey, TValue>.BLACK;
                        y.Color = RedBlackNode<TKey, TValue>.BLACK;
                        // grandparent must be red. Why? Every red node that is not 
                        // a leaf has only black children 
                        x.Parent.Parent.Color = RedBlackNode<TKey, TValue>.RED;
                        x = x.Parent.Parent;	// continue loop with grandparent
                    }
                    else
                    {
                        // uncle is black; determine if x is greater than Parent
                        if (x == x.Parent.Right)
                        {	// yes, x is greater than Parent; rotate Left
                            // make x a Left child
                            x = x.Parent;
                            RotateLeft(x);
                        }
                        // no, x is less than Parent
                        x.Parent.Color = RedBlackNode<TKey, TValue>.BLACK;	// make Parent black
                        x.Parent.Parent.Color = RedBlackNode<TKey, TValue>.RED;		// make grandparent black
                        RotateRight(x.Parent.Parent);					// rotate right
                    }
                }
                else
                {	// x's Parent is on the Right subtree
                    // this code is the same as above with "Left" and "Right" swapped
                    y = x.Parent.Parent.Left;
                    if (y != null && y.Color == RedBlackNode<TKey, TValue>.RED)
                    {
                        x.Parent.Color = RedBlackNode<TKey, TValue>.BLACK;
                        y.Color = RedBlackNode<TKey, TValue>.BLACK;
                        x.Parent.Parent.Color = RedBlackNode<TKey, TValue>.RED;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Left)
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }
                        x.Parent.Color = RedBlackNode<TKey, TValue>.BLACK;
                        x.Parent.Parent.Color = RedBlackNode<TKey, TValue>.RED;
                        RotateLeft(x.Parent.Parent);
                    }
                }
            }
            rbTree.Color = RedBlackNode<TKey, TValue>.BLACK;		// rbTree should always be black
        }

        ///<summary>
        /// RotateLeft
        /// Rebalance the tree by rotating the nodes to the left
        ///</summary>
        private void RotateLeft(RedBlackNode<TKey, TValue> x)
        {
            // pushing node x down and to the Left to balance the tree. x's Right child (y)
            // replaces x (since y > x), and y's Left child becomes x's Right child 
            // (since it's < y but > x).

            RedBlackNode<TKey, TValue> y = x.Right;			// get x's Right node, this becomes y

            // set x's Right link
            x.Right = y.Left;					// y's Left child's becomes x's Right child

            // modify parents
            if (y.Left != sentinelNode)
                y.Left.Parent = x;				// sets y's Left Parent to x

            if (y != sentinelNode)
                y.Parent = x.Parent;			// set y's Parent to x's Parent

            if (x.Parent != null)
            {	// determine which side of it's Parent x was on
                if (x == x.Parent.Left)
                    x.Parent.Left = y;			// set Left Parent to y
                else
                    x.Parent.Right = y;			// set Right Parent to y
            }
            else
                rbTree = y;						// at rbTree, set it to y

            // link x and y 
            y.Left = x;							// put x on y's Left 
            if (x != sentinelNode)						// set y as x's Parent
                x.Parent = y;

            x.Count = x.Left.Count + x.Right.Count + 1;
            y.Count = y.Left.Count + y.Right.Count + 1;
        }
        ///<summary>
        /// RotateRight
        /// Rebalance the tree by rotating the nodes to the right
        ///</summary>
        private void RotateRight(RedBlackNode<TKey, TValue> x)
        {
            // pushing node x down and to the Right to balance the tree. x's Left child (y)
            // replaces x (since x < y), and y's Right child becomes x's Left child 
            // (since it's < x but > y).

            RedBlackNode<TKey, TValue> y = x.Left;			// get x's Left node, this becomes y

            // set x's Right link
            x.Left = y.Right;					// y's Right child becomes x's Left child

            // modify parents
            if (y.Right != sentinelNode)
                y.Right.Parent = x;				// sets y's Right Parent to x

            if (y != sentinelNode)
                y.Parent = x.Parent;			// set y's Parent to x's Parent

            if (x.Parent != null)				// null=rbTree, could also have used rbTree
            {	// determine which side of it's Parent x was on
                if (x == x.Parent.Right)
                    x.Parent.Right = y;			// set Right Parent to y
                else
                    x.Parent.Left = y;			// set Left Parent to y
            }
            else
                rbTree = y;						// at rbTree, set it to y

            // link x and y 
            y.Right = x;						// put x on y's Right
            if (x != sentinelNode)				// set y as x's Parent
                x.Parent = y;

            x.Count = x.Left.Count + x.Right.Count + 1;
            y.Count = y.Left.Count + y.Right.Count + 1;

        }
        ///<summary>
        /// GetData
        /// Gets the data object associated with the specified key
        ///<summary>
        public object GetData(TKey key)
        {
            int result;

            RedBlackNode<TKey, TValue> treeNode = rbTree;     // begin at root

            // traverse tree until node is found
            while (treeNode != sentinelNode)
            {
                result = comparer.Compare(key, treeNode.Key);
                if (result == 0)
                {
                    lastNodeFound = treeNode;
                    return treeNode.Data;
                }
                if (result < 0)
                    treeNode = treeNode.Left;
                else
                    treeNode = treeNode.Right;
            }

            throw (new Exception("RedBlackNode key was not found"));
        }
        ///<summary>
        /// GetMinKey
        /// Returns the minimum key value
        ///<summary>
        public TKey GetMinKey()
        {
            RedBlackNode<TKey, TValue> treeNode = rbTree;

            if (treeNode == null || treeNode == sentinelNode)
                throw (new Exception("RedBlack tree is empty"));

            // traverse to the extreme left to find the smallest key
            while (treeNode.Left != sentinelNode)
                treeNode = treeNode.Left;

            lastNodeFound = treeNode;

            return treeNode.Key;

        }


        ///<summary>
        /// IsEmpty
        /// Is the tree empty?
        ///<summary>
        public bool IsEmpty()
        {
            return (rbTree == null);
        }

        ///<summary>
        /// Delete
        /// Delete a node from the tree and restore red black properties
        ///<summary>
        private void Delete(RedBlackNode<TKey, TValue> z)
        {
            // A node to be deleted will be: 
            //		1. a leaf with no children
            //		2. have one child
            //		3. have two children
            // If the deleted node is red, the red black properties still hold.
            // If the deleted node is black, the tree needs rebalancing

            RedBlackNode<TKey, TValue> x = new RedBlackNode<TKey, TValue>();	// work node to contain the replacement node
            RedBlackNode<TKey, TValue> y;					// work node 

            // find the replacement node (the successor to x) - the node one with 
            // at *most* one child. 
            if (z.Left == sentinelNode || z.Right == sentinelNode)
                y = z;						// node has sentinel as a child
            else
            {
                // z has two children, find replacement node which will 
                // be the leftmost node greater than z
                y = z.Right;				        // traverse right subtree	
                while (y.Left != sentinelNode)		// to find next node in sequence
                    y = y.Left;
            }

            // at this point, y contains the replacement node. it's content will be copied 
            // to the valules in the node to be deleted

            // x (y's only child) is the node that will be linked to y's old parent. 
            if (y.Left != sentinelNode)
                x = y.Left;
            else
                x = y.Right;

            // replace x's parent with y's parent and
            // link x to proper subtree in parent
            // this removes y from the chain
            x.Parent = y.Parent;
            if (y.Parent != null)
                if (y == y.Parent.Left)
                    y.Parent.Left = x;
                else
                    y.Parent.Right = x;
            else
                rbTree = x;			// make x the root node

            // copy the values from y (the replacement node) to the node being deleted.
            // note: this effectively deletes the node. 
            if (y != z)
            {
                z.Key = y.Key;
                z.Data = y.Data;
            }

            if (y.Color == BLACK)
                RestoreAfterDelete(x);

            lastNodeFound = sentinelNode;
        }

        ///<summary>
        /// RestoreAfterDelete
        /// Deletions from red-black trees may destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it
        ///</summary>
        private void RestoreAfterDelete(RedBlackNode<TKey, TValue> x)
        {
            // maintain Red-Black tree balance after deleting node 			

            RedBlackNode<TKey, TValue> y;


            while (x != rbTree && x.Color == BLACK)
            {
                if (x == x.Parent.Left)			// determine sub tree from parent
                {
                    y = x.Parent.Right;			// y is x's sibling 
                    if (y.Color == RED)
                    {	// x is black, y is red - make both black and rotate
                        y.Color = BLACK;
                        x.Parent.Color = RED;
                        RotateLeft(x.Parent);
                        y = x.Parent.Right;
                    }
                    if (y.Left.Color == BLACK &&
                        y.Right.Color == BLACK)
                    {	// children are both black
                        y.Color = RED;		// change parent to red
                        x = x.Parent;					// move up the tree
                    }
                    else
                    {
                        if (y.Right.Color == BLACK)
                        {
                            y.Left.Color = BLACK;
                            y.Color = RED;
                            RotateRight(y);
                            y = x.Parent.Right;
                        }
                        y.Color = x.Parent.Color;
                        x.Parent.Color = BLACK;
                        y.Right.Color = BLACK;
                        RotateLeft(x.Parent);
                        x = rbTree;
                    }
                }
                else
                {	// right subtree - same as code above with right and left swapped
                    y = x.Parent.Left;
                    if (y.Color == RED)
                    {
                        y.Color = BLACK;
                        x.Parent.Color = RED;
                        RotateRight(x.Parent);
                        y = x.Parent.Left;
                    }
                    if (y.Right.Color == BLACK &&
                        y.Left.Color == BLACK)
                    {
                        y.Color = RED;
                        x = x.Parent;
                    }
                    else
                    {
                        if (y.Left.Color == BLACK)
                        {
                            y.Right.Color = BLACK;
                            y.Color = RED;
                            RotateLeft(y);
                            y = x.Parent.Left;
                        }
                        y.Color = x.Parent.Color;
                        x.Parent.Color = BLACK;
                        y.Left.Color = BLACK;
                        RotateRight(x.Parent);
                        x = rbTree;
                    }
                }
            }
            x.Color = BLACK;
        }

        ///<summary>
        /// Clear
        /// Empties or clears the tree
        ///<summary>
        public void Clear()
        {
            rbTree = sentinelNode;
            intCount = 0;
        }
        ///<summary>
        /// Size
        /// returns the size (number of nodes) in the tree
        ///<summary>
        public int Size()
        {
            // number of keys
            return intCount;
        }

        private class RedBlackNode<NKey, NValue>
        {
            // tree node colors
            public static int RED = 0;
            public static int BLACK = 1;
            public int Count = 0; //Number of nodes in this node sub tree including itself

            // key provided by the calling class
            private NKey ordKey;
            // the data or value associated with the key
            private NValue objData;
            // color - used to balance the tree
            private int intColor;
            // left node 
            private RedBlackNode<NKey, NValue> rbnLeft;
            // right node 
            private RedBlackNode<NKey, NValue> rbnRight;
            // parent node 
            private RedBlackNode<NKey, NValue> rbnParent;

            ///<summary>
            ///Key
            ///</summary>
            public NKey Key
            {
                get
                {
                    return ordKey;
                }

                set
                {
                    ordKey = value;
                }
            }
            ///<summary>
            ///Data
            ///</summary>
            public NValue Data
            {
                get
                {
                    return objData;
                }

                set
                {
                    objData = value;
                }
            }
            ///<summary>
            ///Color
            ///</summary>
            public int Color
            {
                get
                {
                    return intColor;
                }

                set
                {
                    intColor = value;
                }
            }
            ///<summary>
            ///Left
            ///</summary>
            public RedBlackNode<NKey, NValue> Left
            {
                get
                {
                    return rbnLeft;
                }

                set
                {
                    rbnLeft = value;
                }
            }
            ///<summary>
            /// Right
            ///</summary>
            public RedBlackNode<NKey, NValue> Right
            {
                get
                {
                    return rbnRight;
                }

                set
                {
                    rbnRight = value;
                }
            }
            public RedBlackNode<NKey, NValue> Parent
            {
                get
                {
                    return rbnParent;
                }

                set
                {
                    rbnParent = value;
                }
            }

            public RedBlackNode()
            {
                Color = RED;
            }
        }
    }
}

