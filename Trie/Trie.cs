using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.DataStructure
{
	/// <summary>
	/// It is a generic Trie DS.
	/// For using it as a dictionary, instantiate it with char as key
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class Trie<TKey,TValue>
	{
		protected TrieNode _root = new TrieNode(default(TKey));

		/// <summary>
		/// Add the new pair of keys and value to Trie.
		/// If the key already exists, override the value.
		/// </summary>
		/// <param name="keys">A chain of keys. Ex. String of chars</param>
		/// <param name="value"></param>
		public void Add(IEnumerable<TKey> keys, TValue value)
		{
			var n = _root;
			foreach (TKey k in keys)
			{
				n.AddChild(k);	// do not add it if already exists
				n = n.Children[k];
			}
			n.Value = value;
		}

		/// <summary>
		/// Remove the value from the data structure
		/// </summary>
		/// <param name="keys">A chain of keys. Ex. String of chars</param>
		public void Remove(IEnumerable<TKey> keys)
		{
			FindNode(keys, _root).ClearValue();
		}

		/// <summary>
		/// Find the value added exactly by provided key
		/// </summary>
		/// <param name="key">A chain of keys. Ex. String of chars</param>
		/// <returns>Return the value or default/null if it is not available</returns>
		public TValue Find(IEnumerable<TKey> keys)
		{
			var result = FindNode(keys, _root);
			if (result != null) return result.Value;
			return default(TValue);
		}


		/// <summary>
		/// Get or set the value by a chain of TKeys
		/// </summary>
		/// <param name="index">A chain of keys. Ex. String of chars</param>
		/// <returns>Return the value or default/null if it is not available</returns>
		public TValue this[IEnumerable<TKey> index]
		{
			get { return Find(index); }
			set { Add(index, value); }
		}

		/// <summary>
		/// Find the node with specified keys starting from node n.
		/// The key and value of n and its parent nodes are not considered.
		/// </summary>
		/// <param name="keys">A chain of keys. Ex. String of chars</param>
		/// <param name="n">The starting node for search</param>
		/// <returns></returns>
		protected TrieNode FindNode(IEnumerable<TKey> keys, TrieNode n)
		{
			foreach (TKey k in keys)
			{
				if (n.Children.ContainsKey(k))
					n = n.Children[k];
				else
					return null;
			}
			return n;
		}

		/// <summary>
		/// Scan the whole tree and find all values that their chain of keys are starting with provided keys.
		/// </summary>
		/// <param name="keys">A chain of keys. Ex. String of chars</param>
		/// <returns></returns>
		public IEnumerable<TValue> FindAllStartingWith(IEnumerable<TKey> keys)
		{
			var q = new Queue<TrieNode>();
			var result = new List<TValue>();
			var n = FindNode(keys, _root);
			if (n == null) return result;

			q.Enqueue(n);
			do
			{
				n = q.Dequeue();
				if (n.IsValueNode) result.Add(n.Value);
				foreach (var ch in n.Children.Values)
				{
					q.Enqueue(ch);
				}
			} while (q.Count > 0);
			return result;
		}

		/// <summary>
		/// Count the number of values stored with a key chain starting with provided keys.
		/// </summary>
		/// <param name="keys">A chain of keys. Ex. String of chars</param>
		/// <returns></returns>
		public int CountAllStartingWith(IEnumerable<TKey> keys)
		{
			var n = FindNode(keys, _root);
			if (n == null) return 0;
			return n.SubTreeValueCount;
		}

		protected class TrieNode
		{
			public TrieNode(TKey key) { _key = key; }

			private TValue _Value;
			public TValue Value
			{
				get { return _Value; }
				set {
					_Value = value;
					if (!IsValueNode)
					{
						UpdateCountToTop(1);
						IsValueNode = true;
					}
				}
			}

			public bool IsValueNode { get; set; }

			private TKey _key;
			public TKey Key { get { return _key; } }

			Dictionary<TKey, TrieNode> _children = new Dictionary<TKey, TrieNode>();
			public Dictionary<TKey, TrieNode> Children { get { return _children; } }

			public int SubTreeValueCount { get; internal set; }

			public TrieNode Parent { get; internal set; }

			public void ClearValue()
			{
				_Value = default(TValue);
				UpdateCountToTop(-1);
				IsValueNode = false;
			}

			/// <summary>
			/// Do not adds it if already exists
			/// </summary>
			/// <param name="k">Key</param>
			public void AddChild(TKey k)
			{
				if (!_children.ContainsKey(k))
				{
					var n = new TrieNode(k);
					_children.Add(k, n);
					n.Parent = this;
				}
			}

			private void UpdateCountToTop(int i)
			{
				var n = this;
				while (n != null)
				{
					n.SubTreeValueCount += i;
					n = n.Parent;
				}
			}
		}
	}
}
