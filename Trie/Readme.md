See [Algorithms, 4th Edition](https://algs4.cs.princeton.edu/52trie/) for definition of and more explanation about TRIE.

This TRIE works with any chain of keys (IEnumerable<TKey>) and not just strings. Also, in addition to regular TRIE functions, this one returns the number of matching prefixes in O(l) where l is the length of the prefix.