using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationSystem
{
    public class Node<T>
    {
        private T data;
        private List<Node<T>> children;
        private Node<T> parentNode;

        public T Data { get { return data; } }
        public List<Node<T>> Children { get { return children; } }
        public Node<T> Parent { get { return parentNode; } }

        public Node()
        {
            data = default(T);
            children = new List<Node<T>>();
            parentNode = null;
        }

        public void SetData(T item)
        {
            data = item;
        }

        public void AddChild(Node<T> item)
        {
            // Add the child into the children list
            children.Add(item);

            // Set the childs parent
            item.SetParent(this);
        }

        public void RemoveChild(Node<T> item)
        {
            // Remove the child from the list of children
            children.Remove(item);

            // Reset the child objects parent
            item.SetParent(null);
        }

        public void SetParent(Node<T> item)
        {
            parentNode = item;
        }
    }

    public class NodeStructure<T>
    {
        private List<Node<T>> items;
        private Node<T> root;

        public Node<T> Root { get { return root; } }

        public NodeStructure()
        {
            items = new List<Node<T>>();
            root = new Node<T>();
        }
    }
}
