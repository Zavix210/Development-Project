using System;
using System.Collections.Generic;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    public class DecisionMaker : SimulationComponentBase
    {
        private DecisionStore store;

        public DecisionMaker(SimulationController controller) : base(controller)
        {
            store = new DecisionStore();
        }

        public void Load(InputObject input)
        {
            // Did the store successfully parse the input data?
            if(store.Load(input))
            {

            }
            else // Failed to load the input data
            {

            }
        }

        public override void OnInitialize()
        {

        }
    }
}
