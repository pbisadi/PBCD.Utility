See [Algorithms, 4th Edition](https://algs4.cs.princeton.edu/home/) by Robert Sedgewick and Kevin Wayne

[Here](https://github.com/pbisadi/Algorithms) is the repository of this package.

The main name space is called  **Algorithms** which includes the following sub name spaces:

* ComputationalGeometry
* DataStructure
* Graphs
* Randomization
* SearchTree
* Sort


- **ComputationalGeometry**
    - ConvexHull: Gets a set of 2D points and returns a subset of them representing the smallest convex polygon that includes all points.
- **DataStructure**
    - PriorityQueue: There are two implemented versions this DS, MaxPriorityQueue and MaxPriorityQueue.
    - QuickUnion: Uses weighted quick-union with path compression to calculate connected components in O(1).
		Initial it by providing the number of components (nodes) and keep connecting them using Union() method. Note that you cannot disconnect component.
		Property Count returns the number of components and node p and q are connected if Find(p) == Find(q)
	- TRIE: This TRIE works with any chain of keys and not just strings. Also, in additional to regular TRIE functions, this one returns number of matching prefixes in O(l) where l is the length of the prefix.
- **Graphs**
    - **Digraph**
        - Digraph: Represents a directed graph. This
        - [TopologicalSort](https://en.wikipedia.org/wiki/Topological_sorting): Gets a digraph and return their IDs sorted topological
        - DirectedBFS: Gets a digraph and the index of the source vertex and calculates the path to every other vertex (if existing) using [Depth-first search](https://en.wikipedia.org/wiki/Depth-first_search) algorithm. HasPathTo(v) costs O(1) to check if there is a path to vertex v.
        - DirectedDFS: Same as DirectedBFS but uses [Breadth-first search](https://en.wikipedia.org/wiki/Breadth-first_search) algorithm.
        - SCC_KosarajuSharir : Uses [Kosaraju Sharir Algorithm](https://en.wikipedia.org/wiki/Kosaraju%27s_algorithm) (TopologicalSort class) to calcualte [Strongly connected component](https://en.wikipedia.org/wiki/Strongly_connected_component)
    - BipartiteGraph: Departs the graph into two groups if possible and returns true. Otherwise returns false
    - BreadthFirstPaths
    - DepthFirstPaths
    - ConnectedComponents
    - Graph
- **Randomization**
    - WeightedRandom: Randomly select specified number of items from the list.The items with higher weight are in the result with higher probability.
- **SearchTree**
    - BinarySearchTree
    - RedBlackTree: It is a Left Leaning Red-Black tree (LLRB Tree)
    - OST: Order Statistics Tree is a RedBlack tree which is able to return the rank of each elements
- **Sort** : All sorts of sorts
