using System;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    public class Node<T>
    {
        private Node<T>[] choices;
        private T data;

        public Node<T>[] Choices { get { return choices; } }
        public T Data { get { return data; } set { data = value; } }

        public Node(Node<T>[] choices)
        {
            this.choices = choices;
        }
    }

    public class DecisionStore<T>
    {
        private List<Node<T>> nodes;
        private Node<T> root;

        public int NodeCount { get { return nodes.Count; } }
        public Node<T> Root { get { return root; } }

        public DecisionStore()
        {
            nodes = new List<Node<T>>();
            root = null;
        }
    }
}
