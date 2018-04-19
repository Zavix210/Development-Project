using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    using DNode = SimulationSystem.DecisionNode<string, string, int>;
    
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
            // TEMPORARY TEST CODE

            DNode n1 = new DNode();
            n1.AddAttribute("TITLE", "Start");
            n1.SetIdentifier(0);
            Decision n1d = new Decision(this, n1);
            n1.SetWrapper(n1d);

            DNode n2 = new DNode();
            n2.SetIdentifier(1);
            n2.AddAttribute("TITLE", "Go Left");
            n2.AddAttribute("IS_FAIL_NODE", "TRUE");
            n2.AddAttribute("OVERRIDE_TITLE", "Go Back");
            Decision n2d = new Decision(this, n2);
            n2.SetWrapper(n2d);

            DNode n3 = new DNode();
            n3.SetIdentifier(2);
            n3.AddAttribute("TITLE", "Go Right");
            Decision n3d = new Decision(this, n3);
            n3.SetWrapper(n3d);

            DNode n4 = new DNode();
            n4.SetIdentifier(2);
            n4.AddAttribute("TITLE", "You Win");
            Decision n4d = new Decision(this, n4);
            n4.SetWrapper(n4d);

            n3.AddRoute(n4.Identifier);

            // Route from 1 to 2/3
            n1.AddRoute(n2.Identifier);
            n1.AddRoute(n3.Identifier);

            // Link back
            n2.AddRoute(n1.Identifier);
            n3.AddRoute(n1.Identifier);

            rootNode = n1;

            store.Add(n1);
            store.Add(n2);
            store.Add(n3);

            // END TEMPORARY TEST CODE

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
                        Load(null);
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

        public bool GetDecision(int choiceID, out Decision decision)
        {
            DNode dNode;
            if(GetNode(choiceID, out dNode))
            {
                decision = dNode.Wrapper;
                return true;
            }
            else
            {
                decision = null;
                return false;
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
                Message resultMessage = new Message((int)MessageDestination.DECISION_CHANGE, "DECISION_VALID", node.Wrapper);
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