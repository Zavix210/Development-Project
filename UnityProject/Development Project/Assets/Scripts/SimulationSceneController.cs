using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SceneBuilderWpf.DataModels;
// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    using SceneNode = SimulationSystem.SimulationSceneNode<string, string, int, ITimelineAction>;
    
    public class SimulationSceneController : SimulationComponentBase
    {
        private List<SceneNode> store;
        private SceneNode currentNode;
        private SceneNode rootNode;

        public SimulationSceneController(SimulationController controller) : base(controller)
        {
            store = new List<SceneNode>();
        }

        // Accept all messages
        public override bool IsMessageRouteValid(int route)
        {
            return true;
        }

        public void Load(Scene input)
        {
            // TEMPORARY TEST CODE

            //SceneNode n1 = new SceneNode();
            //SimulationScene n1d = new SimulationScene(this, n1);
            //n1.SetIdentifier(0);
            //n1.AddAttribute("DURATION", "10.0");

            //TransitionTimelineAction action = new TransitionTimelineAction(1);
            //action.SetTimeOfAction(2.0f);
            //n1.AddAction(action);

            //SceneNode n2 = new SceneNode();
            //SimulationScene n2d = new SimulationScene(this, n2);
            //n2.SetIdentifier(1);
            //n2.AddAttribute("DURATION", "10.0");

            //// Link routes
            //n1.AddRoute(n2.Identifier);
            //n2.AddRoute(n1.Identifier);

            //rootNode = n1;

            //store.Add(n1);
            //store.Add(n2);

            // END TEMPORARY TEST CODE

            // ----------------------------------------------------------------------------------------------------------------------------------
            // INFO
            // ----------------------------------------------------------------------------------------------------------------------------------
            // Identifier is the way to reference other scenes, so it will be pulled from the scene file most likely
            // ----------------------------------------------------------------------------------------------------------------------------------
            // AddAction(...) will add an action, these are classes which can run code at a certain time within the timeline
            // of a scene. For example, DecisionTimelineAction is designed to show the decision nodes on-screen by accessing the UI Controller
            // (Not yet implemented though).
            // ----------------------------------------------------------------------------------------------------------------------------------
            // AddAttribute(...) is to add a unique Key-Value pair which can be pulled by any component.
            // ----------------------------------------------------------------------------------------------------------------------------------
            // SimulationScene is a necessary class which acts as an 'outward-facing' wrapper for a Node
            // which provides access to internals and a few utility methods
            // ----------------------------------------------------------------------------------------------------------------------------------

            // START DEMONSTRATION CODE

            SceneNode testNode = new SceneNode(); // Create Node
            testNode.AddAttribute("DURATION", "10.0"); // DURATION = the length of a scene (length of video most likely)
            testNode.SetIdentifier(74); // Set Unique ID
            store.Add(testNode); // Add the item to the store
            SimulationScene testNodeWrapper = new SimulationScene(this, testNode); // Create the wrapper (outward facing object)

            DecisionTimelineAction decisionAction = new DecisionTimelineAction(); // Create the action
            decisionAction.SetTimeOfAction(2.0f); // Set the action to start at 2.0 seconds in

            DecisionSet set = new DecisionSet(); // Create the decision set
            set.AddDecision(new Decision(DecisionResult.FAIL, "Go Left", "Left is BAD")); // Create the decision choice
            set.AddDecision(new Decision(DecisionResult.SUCCESS, "Go Right", "Right is GOOD")); // Create the decision choice
            decisionAction.SetDecisionSet(set); // Apply the set

            testNode.AddAction(decisionAction); // Add the action to the node

            rootNode = testNode; // Set the root as the test node

            // END DEMONSTRATION CODE

            SceneNode sceneRoot = new SceneNode();
            sceneRoot.AddAttribute("SCENE_FILE", input.SceneFile);
            sceneRoot.AddAttribute("QUESTION_TEXT", input.GeneralSettings.QuestionText);
            sceneRoot.AddAttribute("SCENE_BRIGHTNESS", input.GeneralSettings.SceneBrightness.ToString());
            sceneRoot.AddAttribute("SOUND_VOLUME", input.GeneralSettings.SoundVolume.ToString());
            sceneRoot.SetIdentifier(0);

            //need to make this recursive. Whereyougo contains the linked scene to the decision
            foreach (ScenceChoice choice in input.Choice)
            {
                
            }






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
                        Scene scene = JsonParseUnity.LoadJsonFileIntoScenePage(Application.dataPath + @"\JsonScene\scene.json");
                        Load(scene);
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
                        //int choiceID = (int)message.Data;
                        //SceneNode resultNode;

                        //// Get the node from the choice
                        //if(!GetNode(choiceID, out resultNode))
                        //{
                        //    // Log a warning about the problem
                        //    Debug.LogWarning("Did not find a node in the store for the specified ID: " + choiceID);
                        //}

                        //// Change the node
                        //ChangeNode(resultNode);
                    }
                    break;
            }
        }

        public void ChangeScene(int routeID)
        {
            SceneNode resultNode;

            // Get the node from the choice
            if (!GetNode(routeID, out resultNode))
            {
                // Log a warning about the problem
                Debug.LogError("Did not find a node in the store for the specified ID: " + routeID);
            }

            // Change the node
            ChangeNode(resultNode);
        }

        public bool GetScene(int routeID, out SimulationScene scene)
        {
            SceneNode sNode;
            if(GetNode(routeID, out sNode))
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
                Message resultMessage = new Message((int)MessageDestination.SCENE_CHANGE, "VALID", node.Wrapper);
                Controller.PropagateMessage(resultMessage);
            }
            else // Node is NULL, it's invalid
            {
                // Pass a message notifying any components that the decision was INVALID
                Message resultMessage = new Message((int)MessageDestination.SCENE_CHANGE, "INVALID", null);
                Controller.PropagateMessage(resultMessage);
            }
        }

        private void Reset()
        {
            ChangeNode(rootNode);
        }

        SceneNode CreateSceneNode(int idValue)
        {
            SceneNode node = new SceneNode();
            node.SetIdentifier(idValue);
            store.Add(node);
            SimulationScene wrapper = new SimulationScene(this, node);

            return node;
        }
    }
}