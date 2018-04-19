using UnityEngine;
using System.Collections.Generic;

namespace SimulationSystem
{
    using SceneNode = SimulationSystem.SimulationSceneNode<string, string, int>;

    public class SimulationSceneNode<KeyType, ValueType, IndexType>
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

        /// <summary>
        /// The Identifier is the unique identification method of the decision.
        /// </summary>
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
            routes = new List<IndexType>();
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
            if (!routes.Contains(route))
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

    enum RouteType
    {
        Scene_Transition,

    }

    class Route
    {

    }

    public class SimulationScene
    {
        private DecisionMaker decisionMaker;
        private SceneNode wrappedScene;

        public int Identifier { get { return wrappedScene.Identifier; } }

        public SimulationScene(DecisionMaker decisionMaker, SceneNode wrappedScene)
        {
            this.decisionMaker = decisionMaker;
            this.wrappedScene = wrappedScene;

            // Set the nodes wrapper to this
            wrappedScene.SetWrapper(this);
        }

        public string GetDisplayTitle()
        {
            // Is self-node contained within one of the child nodes? (loop-back case)
            SimulationScene parentNode;
            if (GetParentFromChildren(out parentNode))
            {
                // Get the override title
                string str;
                if (GetAttribute("OVERRIDE_TITLE", out str))
                {
                    return str;
                }
                else // Failed to get override titlae
                {
                    Debug.LogWarning("Failed to get the override title");
                    return GetDefaultTitle();
                }
            }
            else // Normal node
            {
                return GetDefaultTitle();
            }

            //// Is the node classed as a failure?
            //string str;
            //if (GetAttribute("IS_FAIL_NODE", out str))
            //{
            //    // Get the override title
            //    if (GetAttribute("OVERRIDE_TITLE", out str))
            //    {
            //        return str;
            //    }
            //    else // Failed to get override titlae
            //    {
            //        Debug.LogWarning("Failed to get the override title");
            //        return GetDefaultTitle();
            //    }
            //}
            //else // The node is not a fail node, get the default title
            //{
            //    return GetDefaultTitle();
            //}
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

        public string GetDefaultTitle()
        {
            // Get the default title
            string str;
            if (GetAttribute("TITLE", out str))
            {
                return str;
            }
            else
            {
                Debug.LogWarning("Failed to get the default title");
                return "MISSING_TITLE";
            }
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
            return decisionMaker.GetDecision(route, out scene);
        }

        public bool GetAttribute(string key, out string value)
        {
            return wrappedScene.GetAttribute(key, out value);
        }
    }
}