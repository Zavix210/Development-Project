﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    using DNode = SimulationSystem.Decision<string, string, int>;

    public class Decision<KeyType, ValueType, IndexType>
    {
        /// <summary>
        /// Attributes are the individual data elements of the decision,
        /// they can vary from decision to decision.
        /// </summary>
        private Dictionary<KeyType, ValueType> attributes;

        /// <summary>
        /// Routes are the identifiers of the decisions which can be traversed
        /// to through the decision system.
        /// </summary>
        private List<IndexType> routes;

        private IndexType identifier;

        public IndexType Identifier { get { return identifier; } }

        public Decision()
        {
            attributes = new Dictionary<KeyType, ValueType>();
            routes = new List<IndexType>();
        }

        public void SetIdentifier(IndexType identifier)
        {
            this.identifier = identifier;
        }

        public bool AddRoute(IndexType route)
        {
            if(!routes.Contains(route))
            {
                routes.Add(route);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveRoute(IndexType route)
        {
            return routes.Remove(route);
        }

        public List<IndexType> GetRoutes()
        {
            return routes;
        }

        public bool AddAttribute(KeyType key, ValueType value)
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

        public bool RemoveAttribute(KeyType key)
        {
            return attributes.Remove(key);
        }

        public bool GetAttribute(KeyType key, out ValueType value)
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

        public bool ContainsAttribute(KeyType key)
        {
            return attributes.ContainsKey(key);
        }
    }

    public class DecisionMaker : SimulationComponentBase
    {
        private List<DNode> store;
        private DNode currentNode;
        private DNode rootNode;

        public DecisionMaker(SimulationController controller) : base(controller)
        {
            store = new List<DNode>();
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
                case (int)MessageDestination.SIMULATION_START: // Simulation has started
                    {
                        Reset();
                    }
                    break;
                case (int)MessageDestination.SIMULATION_END: // Simulation has ended
                    {
                        Reset();
                    }
                    break;
                case (int)MessageDestination.DECISION_UI_CHOICE: // UI Button pressed
                    {
                        int choiceID = (int)message.Data;
                        DNode resultNode;

                        // Get the node from the choice
                        if(!GetNode(choiceID, out resultNode))
                        {
                            // Log a warning about the problem
                            Debug.LogWarning("Did not find a node in the store for the specified ID: " + choiceID);
                        }

                        // Change the node
                        ChangeNode(resultNode);
                    }
                    break;
            }
        }

        private bool GetNode(int choiceID, out DNode nextNode)
        {
            // Try to find the desired node
            foreach(DNode node in store)
            {
                // Is this node the desired node?
                if (node.Identifier == choiceID)
                {
                    nextNode = node;
                    return true;
                }
            }

            // The node wasn't found
            nextNode = null;
            return false;
        }

        private void ChangeNode(DNode node)
        {
            // Is the node valid?
            if (node != null)
            {
                // Pass a message notifying any components that the decision was VALID
                Message resultMessage = new Message((int)MessageDestination.DECISION_CHANGE, "DECISION_VALID", node);
                Controller.PropagateMessage(resultMessage);
            }
            else // Node is NULL, it's invalid
            {
                // Pass a message notifying any components that the decision was INVALID
                Message resultMessage = new Message((int)MessageDestination.DECISION_CHANGE, "DECISION_INVALID", null);
                Controller.PropagateMessage(resultMessage);
            }
        }

        private void Reset()
        {
            ChangeNode(rootNode);
        }
    }
}