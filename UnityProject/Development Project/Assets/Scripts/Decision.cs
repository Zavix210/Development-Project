using UnityEngine;
using System.Collections.Generic;

namespace SimulationSystem
{
    using DNode = SimulationSystem.DecisionNode<string, string, int>;

    public class DecisionNode<KeyType, ValueType, IndexType>
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
        private Decision wrapper;

        public IndexType Identifier { get { return identifier; } }
        public Decision Wrapper { get { return wrapper; } }

        public DecisionNode()
        {
            attributes = new Dictionary<KeyType, ValueType>();
            routes = new List<IndexType>();
        }

        public void SetWrapper(Decision wrapper)
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

    public class Decision
    {
        private DecisionMaker decisionMaker;
        private DNode wrappedDecision;

        public int Identifier { get { return wrappedDecision.Identifier; } }

        public Decision(DecisionMaker decisionMaker, DNode decision)
        {
            this.decisionMaker = decisionMaker;
            wrappedDecision = decision;
        }

        public string GetDisplayTitle()
        {
            // Is self-node contained within one of the child nodes? (loop-back case)
            Decision parentNode;
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

        private bool GetParentFromChildren(out Decision parent)
        {
            List<int> rawRoutes = wrappedDecision.GetRoutes();
            parent = null;

            // Iterate over all child route IDs
            foreach (int route in rawRoutes)
            {
                bool flag = false;

                // Get the decision node from the route ID
                Decision decision;
                if (GetDecisionFromRoute(route, out decision))
                {
                    // Iterate over the routes in the child to check if this node is contained within it (a loop-back case)
                    List<int> pRawRoutes = decision.wrappedDecision.GetRoutes();
                    foreach (int pRoute in pRawRoutes)
                    {
                        // Get the child node
                        Decision pDecision;
                        if (GetDecisionFromRoute(pRoute, out pDecision))
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

            List<int> rawRoutes = wrappedDecision.GetRoutes();

            // Copy elements into the provided route list
            foreach (int i in rawRoutes)
            {
                routes.Add(i);
            }
        }

        public bool GetDecisionFromRoute(int route, out Decision decision)
        {
            return decisionMaker.GetDecision(route, out decision);
        }

        public bool GetAttribute(string key, out string value)
        {
            return wrappedDecision.GetAttribute(key, out value);
        }
    }
}