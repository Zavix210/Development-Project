using System;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using NodeObject = System.Object;
using InputObject = System.Object;

namespace SimulationSystem
{
    public class DecisionStore
    {
        private List<NodeObject> nodes;
        private NodeObject root;

        public int NodeCount { get { return nodes.Count; } }
        public NodeObject Root { get { return root; } }

        public DecisionStore()
        {
            nodes = new List<object>();
            root = null;
        }

        public bool Load(InputObject data)
        {
            // TODO: Implement data loading

            return true;
        }
    }
}
