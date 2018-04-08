using UnityEngine;

// TODO: These will allow compilation while input middle-man library is being developed.
using InputObject = System.Object;

namespace SimulationSystem
{
    public class DecisionMaker : SimulationComponentBase
    {
        private DecisionStore store;
        private DecisionNode<Decision> currentNode;

        public DecisionMaker(SimulationController controller) : base(controller)
        {
            store = new DecisionStore();
        }

        public override bool IsMessageRouteValid(int route)
        {
            return true;
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

        public override void OnReceivedMessage(Message message)
        {
            int routeID = message.Route;
            switch (routeID)
            {
                case(int)MessageDestination.SIMULATION_ROUTE:
                    Debug.Log("Received simulation message with ID " + message.Identifier);
                    break;
                case (int)MessageDestination.DECISION_CHANGE:

                    break;
            }
        }

        public void SetCurrentNode(DecisionNode<Decision> node)
        {
            currentNode = node;
        }
    }
}
