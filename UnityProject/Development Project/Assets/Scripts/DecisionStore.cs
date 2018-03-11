using System;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    public class DecisionNode
    {
        private DecisionNode[] choices;

        // TODO: Decision data must be changed from object type to a more useful type
        // Decision data
        private object data;

        public DecisionNode[] Choices { get { return choices; } }

        public DecisionNode(DecisionNode[] choices)
        {
            this.choices = choices;
        }
    }

    public class DecisionStore
    {
        private List<DecisionNode> nodes;
        private DecisionNode root;

        public int NodeCount { get { return nodes.Count; } }
        public DecisionNode Root { get { return root; } }

        public DecisionStore()
        {
            nodes = new List<DecisionNode>();
            root = null;
        }

        public bool Load(InputObject data)
        {
            // TODO: Implement data loading

            return true;
        }
    }
}
