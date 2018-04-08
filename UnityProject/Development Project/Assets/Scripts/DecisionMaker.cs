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
            return route == 7;
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
            Debug.Log("Received Message");
        }

        public void SetCurrentNode(DecisionNode<Decision> node)
        {
            currentNode = node;
        }
    }
}
