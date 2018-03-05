using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignProject
{
    public class DecisionMaker : SimulationComponentBase
    {
        private DecisionTree decisionTree;

        public DecisionMaker(SimulationController controller) : base(controller)
        {

        }

        public void SetDecisionTree(DecisionTree decisionTree)
        {
            //...
        }
    }
}