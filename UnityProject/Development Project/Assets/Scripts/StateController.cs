using System;
using SimulationSystem;

public class StateController : SimulationComponentBase, IStateListener<SimulationState>
{
    private StateMachine<SimulationState> stateMachine;

    public SimulationState CurrentState { get { return stateMachine.CurrentState; } }

    public StateController(SimulationController controller) : base(controller)
    {
        stateMachine = new StateMachine<SimulationState>(SimulationState.Loading);
        stateMachine.AddListener(this);
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {

    }

    public void SetState(SimulationState state)
    {
        stateMachine.SetState(state);
    }

    public void StateChanged(SimulationState oldState, SimulationState newState)
    {
        // Ensure states actually changed
        if (oldState != newState)
        {
            // Loading finished
            if (oldState == SimulationState.Loading)
            {
                // Propagate a message to all listeners to notify them that the simulation has loaded.
                Message message = new Message((int)MessageDestination.SIMULATION_LOADING, "", null);
                Controller.PropagateMessage(message);
            }
            else
            {
                if (newState == SimulationState.Simulating) // Menu -> Simulation (Started simulation)
                {
                    // Propagate a message to all listeners to notify them that the simulation has started.
                    Message message = new Message((int)MessageDestination.SIMULATION_START, "", null);
                    Controller.PropagateMessage(message);
                }
                else if (newState == SimulationState.Menu) // Simulation -> Menu (Finished simulation)
                {
                    // Propagate a message to all listners to notify them that the simulation has ended.
                    Message message = new Message((int)MessageDestination.SIMULATION_END, "", null);
                    Controller.PropagateMessage(message);
                }
            }
        }

    }
}
