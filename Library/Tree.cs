using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Library
{
    /// <summary>
    /// A leaf of a tree
    /// </summary>
    /// <typeparam name="E">data type content</typeparam>
    [Serializable]
    public class Leaf<E> : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for element
        /// </summary>
        protected static readonly string elementName = "E";
        /// <summary>
        /// Index name for boolean selected
        /// </summary>
        protected static readonly string isSelectedName = "selected";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor initialization with an element
        /// </summary>
        /// <param name="element">element</param>
        public Leaf(E element)
        {
            this.Set(elementName, element);
        }

        /// <summary>
        /// Emtpy constructor
        /// </summary>
        public Leaf()
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="from">from this leaf</param>
        public Leaf(Leaf<E> from)
        {
            this.Set(elementName, from.Object);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the object in a leaf node
        /// element can be null
        /// </summary>
        public E Object
        {
            get { return this.Get(elementName); }
            set { this.Set(elementName, value); }
        }

        /// <summary>
        /// Gets or sets if it is a selected leaf
        /// </summary>
        public bool IsSelected
        {
            get { return this.Get(isSelectedName, false); }
            set { this.Set(isSelectedName, value); }

        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new Leaf<E>(this);
        }

        #endregion

    }

    /// <summary>
    /// A node in a tree
    /// </summary>
    /// <typeparam name="T">data type element of node list</typeparam>
    /// <typeparam name="E">data type element leaf</typeparam>
    [Serializable]
    public class Node<T, E> : Marshalling.PersistentDataObject, ICloneable where T : IEquatable<T>
    {

        #region Private Fields

        /// <summary>
        /// Index name for sub-node list of type Node&lt;T,E&gt;
        /// </summary>
        protected static readonly string subNodeListName = "subNodes";
        /// <summary>
        /// Index name for element list of type Leaf&lt;E&gt;
        /// </summary>
        protected static readonly string elementListName = "elements";
        /// <summary>
        /// Index name for boolean selected
        /// </summary>
        protected static readonly string isSelectedName = "selected";
        /// <summary>
        /// Index name for node value (T)
        /// </summary>
        protected static readonly string elementNodeName = "nodeValue";
        /// <summary>
        /// Index name for the unique parent node
        /// </summary>
        protected static readonly string parentNodeName = "parent";

        #endregion

        #region Public Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Node()
        {
            this.Set(subNodeListName, new List<Node<T, E>>());
            this.Set(elementListName, new List<Leaf<E>>());
        }

        /// <summary>
        /// Constructor with a node value
        /// </summary>
        /// <param name="nodeValue">node value</param>
        public Node(T nodeValue)
            : this()
        {
            this.Set(elementNodeName, nodeValue);
        }

        /// <summary>
        /// Constructor with a leaf element
        /// can contains nodes
        /// </summary>
        /// <param name="nodeValue">node value</param>
        /// <param name="leaf">first element leaf</param>
        public Node(T nodeValue, Leaf<E> leaf)
            : this(nodeValue)
        {
            this.ListOfElement.Add(leaf);
        }

        /// <summary>
        /// Constructor with an enumerable leaf list
        /// </summary>
        /// <param name="nodeValue">node value</param>
        /// <param name="leafs">leaf enumeration</param>
        public Node(T nodeValue, IEnumerable<Leaf<E>> leafs)
            : this(nodeValue)
        {
            this.ListOfElement.AddRange(leafs);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="from">element to copy</param>
        public Node(Node<T, E> from)
            : this(from.NodeValue)
        {
            foreach (Leaf<E> l in from.ListOfElement)
            {
                this.ListOfElement.Add(l.Clone() as Leaf<E>);
            }
            Task.Factory.StartNew(() =>
            {
                foreach (Node<T, E> n in from.SubNodes)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Node<T, E> copy = n.Clone() as Node<T, E>;
                        // Changes the parent to this
                        copy.Set(parentNodeName, this);
                        this.AddNode(copy);
                    }, TaskCreationOptions.AttachedToParent);
                }
            }).Wait();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the unique parent of this node
        /// </summary>
        public Node<T, E> Parent
        {
            get { return this.Get(parentNodeName); }
        }

        /// <summary>
        /// Gets or sets the node value
        /// </summary>
        public T NodeValue
        {
            get
            {
                return this.Get(elementNodeName);
            }
            set
            {
                this.Set(elementNodeName, value);
            }
        }

        /// <summary>
        /// Gets the enumerable node elements
        /// </summary>
        public IEnumerable<Node<T, E>> SubNodes
        {
            get
            {
                return this.Get(subNodeListName);
            }
        }

        /// <summary>
        /// Gets enumerable leaf elements
        /// </summary>
        public IEnumerable<Leaf<E>> Elements
        {
            get
            {
                return this.Get(elementListName);
            }
        }

        /// <summary>
        /// Gets the list of element
        /// </summary>
        internal List<Leaf<E>> ListOfElement
        {
            get
            {
                return this.Get(elementListName);
            }
        }

        /// <summary>
        /// Gets the list of sub nodes
        /// </summary>
        protected List<Node<T, E>> ListOfNodes
        {
            get
            {
                return this.Get(subNodeListName);
            }
        }

        /// <summary>
        /// Gets or sets if it is a selected node
        /// </summary>
        public bool IsSelected
        {
            get { return this.Get(isSelectedName, false); }
            set { this.Set(isSelectedName, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a node
        /// </summary>
        /// <param name="node">node</param>
        public void AddNode(Node<T, E> node)
        {
            node.Set(parentNodeName, this);
            this.ListOfNodes.Add(node);
        }

        /// <summary>
        /// Add a leaf
        /// </summary>
        /// <param name="leaf">leaf to add</param>
        public void AddLeaf(E leaf)
        {
            this.ListOfElement.Add(new Leaf<E>(leaf));
        }

        /// <summary>
        /// Enumerate selected nodes and add to the binding list
        /// </summary>
        /// <param name="isSelected">true if parent node is selected</param>
        /// <param name="path">complete path of this node</param>
        /// <param name="b">binding list input</param>
        public void EnumerateSelected(bool isSelected, IEnumerable<T> path, BindingList<KeyValuePair<IEnumerable<T>, E>> b)
        {
            List<Node<T, E>>.Enumerator eNode = this.GetNodesEnumerator();
            while (eNode.MoveNext())
            {
                if (eNode.Current.IsSelected || isSelected)
                    eNode.Current.EnumerateSelected(true, path.Concat(new T[] { eNode.Current.NodeValue }), b);
            }
            List<Leaf<E>>.Enumerator eLeaf = this.GetLeafEnumerator();
            while(eLeaf.MoveNext())
            {
                if (eLeaf.Current.IsSelected || isSelected)
                    b.Add(new KeyValuePair<IEnumerable<T>, E>(path, eLeaf.Current.Object));
            }
        }

        /// <summary>
        /// Search an element
        /// </summary>
        /// <param name="search">element successors</param>
        /// <returns>the existing node</returns>
        public Node<T, E> Find(T search)
        {
            Node<T, E> current = this;
            Node<T, E> next = current.ListOfNodes.Find(x => x.NodeValue.Equals(search));
            if (next != null)
                return next;
            else
            {
                next = new Node<T, E>(search);
                current.AddNode(next);
                return next;
            }
        }

        /// <summary>
        /// Search an element
        /// </summary>
        /// <param name="search">element successors</param>
        /// <returns>the existing node</returns>
        public Node<T, E> Find(IEnumerable<T> search)
        {
            Node<T, E> current = this;
            int index = 0;
            int len = search.Count();
            for(; index < len; ++index)
            {
                T e = search.ElementAt(index);
                Node<T, E> next = current.ListOfNodes.Find(x => x.NodeValue.Equals(e));
                if (next != null)
                    current = next;
                else
                    break;
            }
            if (index < len)
            {
                for (; index < len; ++index)
                {
                    T e = search.ElementAt(index);
                    Node<T, E> newNode = new Node<T, E>(e);
                    current.AddNode(newNode);
                    current = newNode;
                }
            }
            return current;
        }

        /// <summary>
        /// Gets the enumerator of sub nodes
        /// </summary>
        /// <returns></returns>
        public List<Node<T, E>>.Enumerator GetNodesEnumerator()
        {
            return this.ListOfNodes.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator of sub nodes
        /// </summary>
        /// <returns></returns>
        public List<Leaf<E>>.Enumerator GetLeafEnumerator()
        {
            return this.ListOfElement.GetEnumerator();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new Node<T, E>(this);
        }

        #endregion
    }

    /// <summary>
    /// A tree class
    /// </summary>
    /// <typeparam name="T">data type element of node</typeparam>
    /// <typeparam name="E">data type element of leaf</typeparam>
    public class Tree<T, E> where T : IEquatable<T>
    {
        #region Private Fields

        Stack<Node<T, E>> _parents;
        Node<T, E> _root;
        Node<T, E> _current;
        Node<T, E> _lastNode;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Constructor with no argument
        /// </summary>
        public Tree(T firstValue)
        {
            this._parents = new Stack<Node<T, E>>();
            this._root = new Node<T, E>(firstValue);
            this._current = this._root;
            this._lastNode = null;
        }

        /// <summary>
        /// Constructs a new tree and attach a node as root
        /// </summary>
        /// <param name="node">node tree</param>
        public Tree(Node<T, E> node)
        {
            this._parents = new Stack<Node<T, E>>();
            this._root = node;
            this._current = this._root;
            this._lastNode = null;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="from">tree to copy</param>
        public Tree(Tree<T, E> from)
        {
            this._root = from._root.Clone() as Node<T, E>;
            Node<T, E> e = from._current;
            Stack<T> s = new Stack<T>();
            while (!e.NodeValue.Equals(from._root.NodeValue))
            {
                s.Push(e.NodeValue);
                e = e.Parent;
            }
            this._current = this._root.Find(s.AsEnumerable());
            e = from._lastNode;
            s = new Stack<T>();
            while (!e.NodeValue.Equals(from._root.NodeValue))
            {
                s.Push(e.NodeValue);
                e = e.Parent;
            }
            this._lastNode = this._root.Find(s.AsEnumerable());
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Root node tree
        /// </summary>
        public Node<T, E> Root
        {
            get { return this._root; }
        }
        /// <summary>
        /// Current node
        /// </summary>
        public Node<T, E> Current
        {
            get { return this._current; }
        }

        /// <summary>
        /// Number of root nodes
        /// </summary>
        public int RootCount
        {
            get { return this._root.SubNodes.Count(); }
        }

        /// <summary>
        /// Inserted last node 
        /// </summary>
        public Node<T, E> Recent
        {
            get { return this._lastNode; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enumerate all nodes and sub nodes
        /// if one is selected, add into the supplied binding list
        /// </summary>
        /// <param name="b">binding list input</param>
        /// <param name="all">true if automatic 'all selected'</param>
        public void EnumerateSelected(BindingList<KeyValuePair<IEnumerable<T>, E>> b, bool all = false)
        {
            this._root.EnumerateSelected(all, new T[] { this._root.NodeValue }, b);
        }

        /// <summary>
        /// Enumerate nodes
        /// </summary>
        /// <returns>an enumerator</returns>
        public List<Node<T, E>>.Enumerator GetEnumerator()
        {
            return this._root.GetNodesEnumerator();
        }

        /// <summary>
        /// Adds a new node data content object as a child of the current node
        /// </summary>
        /// <param name="obj">node object</param>
        public void Add(T obj)
        {
            Node<T, E> node = new Node<T, E>(obj);
            this._lastNode = node;
            this._current.AddNode(node);
        }

        /// <summary>
        /// Add a new leaf data content object as a child of the current node
        /// </summary>
        /// <param name="obj">leaf object</param>
        public void Add(E obj)
        {
            this._current.AddLeaf(obj);
        }

        /// <summary>
        /// Move into the inserted last child of the current node
        /// </summary>
        public void Push()
        {
            this._parents.Push(this._current);
            this._current = this._lastNode;
            this._lastNode = null;
        }

        /// <summary>
        /// Up move
        /// </summary>
        public void Pop()
        {
            this._current = this._parents.Pop();
        }

        #endregion
    }
}
