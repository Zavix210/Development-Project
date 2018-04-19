using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    using SceneNode = SimulationSystem.SimulationSceneNode<string, string, int>;
    
    public class DecisionMaker : SimulationComponentBase
    {
        private List<SceneNode> store;
        private SceneNode currentNode;
        private SceneNode rootNode;

        public DecisionMaker(SimulationController controller) : base(controller)
        {
            store = new List<SceneNode>();
        }

        // Accept all messages
        public override bool IsMessageRouteValid(int route)
        {
            return true;
        }

        public void Load(InputObject input)
        {
            // TEMPORARY TEST CODE

            SceneNode n1 = new SceneNode();
            n1.AddAttribute("TITLE", "Start");
            n1.SetIdentifier(0);
            SimulationScene n1d = new SimulationScene(this, n1);

            SceneNode n2 = new SceneNode();
            n2.SetIdentifier(1);
            n2.AddAttribute("TITLE", "Go Left");
            n2.AddAttribute("IS_FAIL_NODE", "TRUE");
            n2.AddAttribute("OVERRIDE_TITLE", "Go Back");
            SimulationScene n2d = new SimulationScene(this, n2);

            SceneNode n3 = new SceneNode();
            n3.SetIdentifier(2);
            n3.AddAttribute("TITLE", "Go Right");
            SimulationScene n3d = new SimulationScene(this, n3);

            SceneNode n4 = new SceneNode();
            n4.SetIdentifier(2);
            n4.AddAttribute("TITLE", "You Win");
            SimulationScene n4d = new SimulationScene(this, n4);

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
                        SceneNode resultNode;

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

        public bool GetDecision(int choiceID, out SimulationScene scene)
        {
            SceneNode sNode;
            if(GetNode(choiceID, out sNode))
            {
                scene = sNode.Wrapper;
                return true;
            }
            else
            {
                scene = null;
                return false;
            }
        }

        private bool GetNode(int choiceID, out SceneNode nextNode)
        {
            // Try to find the desired node
            foreach(SceneNode node in store)
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

        private void ChangeNode(SceneNode node)
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