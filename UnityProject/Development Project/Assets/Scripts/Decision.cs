using UnityEngine;
using System.Collections.Generic;

namespace SimulationSystem
{
    using DNode = SimulationSystem.DecisionNode<string, string, int>;

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