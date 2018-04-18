using UnityEngine;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    // Simplify the name and define the storage type into a simpler name
    using DNode = SimulationSystem.Decision<string, string, int>;

    public class Decision<KeyType, ValueType, IndexType>
    {
        private Dictionary<KeyType, ValueType> attributes;
        private List<IndexType> routes;

        public Decision()
        {
            attributes = new Dictionary<KeyType, ValueType>();
            routes = new List<IndexType>();
        }

        bool AddAttribute(KeyType key, ValueType value)
        {
            // Is the attribute not already stored?
            if(!ContainsAttribute(key))
            {
                attributes.Add(key, value);
                return true;
            }
            else // Attribute already exists
            {
                return false;
            }
        }

        bool RemoveAttribute(KeyType key)
        {
            return attributes.Remove(key);
        }

        bool GetAttribute(KeyType key, out ValueType value)
        {
            // Try to get the attribute
            if(attributes.TryGetValue(key, out value))
            {
                return true;
            }
            else // Attribute not found
            {
                return false;
            }
        }

        bool ContainsAttribute(KeyType key)
        {
            return attributes.ContainsKey(key);
        }
    }

    public class DecisionMaker : SimulationComponentBase
    {
        private DecisionStore<DNode> store;
        private Node<DNode> currentNode;

        public DecisionMaker(SimulationController controller) : base(controller)
        {
            store = new DecisionStore<DNode>();
        }

        // Accept all messages
        public override bool IsMessageRouteValid(int route)
        {
            return true;
        }

        public void Load(InputObject input)
        {
            // TODO: Parse some input data to populate nodes
        }

        public override void OnInitialize()
        {

        }

        public override void OnReceivedMessage(Message message)
        {
            int routeID = message.Route;
            switch (routeID)
            {
                case (int)MessageDestination.SIMULATION_ROUTE:
                    Debug.Log("Received simulation message with ID " + message.Identifier);
                    break;
                case (int)MessageDestination.DECISION_CHANGE:
                    Debug.Log("Received decision message with ID " + message.Identifier + " of choice " + message.Data);
                    break;
            }
        }

        public void SetCurrentNode(Node<DNode> node)
        {
            currentNode = node;
        }
    }
}