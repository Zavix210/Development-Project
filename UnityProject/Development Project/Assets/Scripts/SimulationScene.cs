using UnityEngine;
using System.Collections.Generic;

namespace SimulationSystem
{
    using SceneNode = SimulationSystem.SimulationSceneNode<string, string, int, ITimelineAction>;

    public class SimulationSceneNode<KeyType, ValueType, IndexType, ActionType>
    {
        private Dictionary<KeyType, ValueType> attributes;
        private Store<IndexType> routes;
        private Store<ActionType> actions;
        private IndexType identifier;

        /// <summary>
        /// An instance of a wrapped decision which contains this.
        /// </summary>
        private SimulationScene wrapper;

        public IndexType Identifier { get { return identifier; } }
        public SimulationScene Wrapper { get { return wrapper; } }

        public SimulationSceneNode()
        {
            attributes = new Dictionary<KeyType, ValueType>();
            routes = new Store<IndexType>();
            actions = new Store<ActionType>();
        }

        public void SetWrapper(SimulationScene wrapper)
        {
            this.wrapper = wrapper;
        }

        public void SetIdentifier(IndexType identifier)
        {
            this.identifier = identifier;
        }

        public bool AddRoute(IndexType route)
        {
            return routes.Add(route);
        }

        public bool RemoveRoute(IndexType route)
        {
            return routes.Remove(route);
        }

        public List<IndexType> GetRoutes()
        {
            return routes.GetRawList();
        }

        public bool AddAction(ActionType action)
        {
            return actions.Add(action);
        }

        public bool RemoveAction(ActionType action)
        {
            return actions.Remove(action);
        }

        public List<ActionType> GetActions()
        {
            return actions.GetRawList();
        }

        public bool AddAttribute(KeyType key, ValueType value)
        {
            // Is the attribute not already stored?
            if (!ContainsAttribute(key))
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
            if (attributes.TryGetValue(key, out value))
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

    public class SimulationScene
    {
        private SimulationSceneController sceneController;
        private SceneNode wrappedScene;

        public int Identifier { get { return wrappedScene.Identifier; } }

        public SimulationScene(SimulationSceneController sceneController, SceneNode wrappedScene)
        {
            this.sceneController = sceneController;
            this.wrappedScene = wrappedScene;

            // Set the nodes wrapper to this
            wrappedScene.SetWrapper(this);
        }
        
        private bool GetParentFromChildren(out SimulationScene parent)
        {
            List<int> rawRoutes = wrappedScene.GetRoutes();
            parent = null;

            // Iterate over all child route IDs
            foreach (int route in rawRoutes)
            {
                bool flag = false;

                // Get the decision node from the route ID
                SimulationScene decision;
                if (GetSceneFromRoute(route, out decision))
                {
                    // Iterate over the routes in the child to check if this node is contained within it (a loop-back case)
                    List<int> pRawRoutes = decision.wrappedScene.GetRoutes();
                    foreach (int pRoute in pRawRoutes)
                    {
                        // Get the child node
                        SimulationScene pDecision;
                        if (GetSceneFromRoute(pRoute, out pDecision))
                        {
                            // Is this node equal to self?
                            if (pDecision.Identifier == Identifier)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }

                    // Was the self-node found in the decision?
                    if (flag)
                    {
                        parent = decision;
                        break;
                    }
                }
            }

            return parent != null;
        }

        public void GetRoutes(List<int> routes, bool autoClear = true)
        {
            // Should the routes be automatically cleared
            if (autoClear)
            {
                routes.Clear();
            }

            List<int> rawRoutes = wrappedScene.GetRoutes();

            // Copy elements into the provided route list
            foreach (int i in rawRoutes)
            {
                routes.Add(i);
            }
        }

        public bool GetSceneFromRoute(int route, out SimulationScene scene)
        {
            return sceneController.GetScene(route, out scene);
        }

        public bool GetAttribute(string key, out string value)
        {
            return wrappedScene.GetAttribute(key, out value);
        }
    }
}