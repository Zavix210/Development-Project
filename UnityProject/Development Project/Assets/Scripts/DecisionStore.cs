using System;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    // TODO: implement details of the decision class
    public class Decision
    {
        public Decision()
        {

        }
    }

    public class DecisionNode<T>
    {
        private DecisionNode<T>[] choices;
        private T data;

        public DecisionNode<T>[] Choices { get { return choices; } }
        public T Data { get { return data; } set { data = value; } }

        public DecisionNode(DecisionNode<T>[] choices)
        {
            this.choices = choices;
        }
    }

    public class DecisionStore
    {
        private List<DecisionNode<Decision>> nodes;
        private DecisionNode<Decision> root;

        public int NodeCount { get { return nodes.Count; } }
        public DecisionNode<Decision> Root { get { return root; } }

        public DecisionStore()
        {
            nodes = new List<DecisionNode<Decision>>();
            root = null;
        }

        public bool Load(InputObject data)
        {
            // TODO: Implement data loading

            return true;
        }
    }
}
