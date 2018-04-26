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
            if (oldState == SimulationState.Loading) // Loading -> Something
            {
                // Propagate a message to all listeners to notify them
                Message message = new Message((int)MessageDestination.SIMULATION_LOADED, "", null);
                Controller.PropagateMessage(message);
            }
            else if (oldState == SimulationState.Menu) // Menu -> Something
            {
                if (newState == SimulationState.Simulating) // Menu -> Simulating
                {
                    // Propagate a message to all listeners to notify them
                    Message message = new Message((int)MessageDestination.SIMULATION_START, "", null);
                    Controller.PropagateMessage(message);
                }
            }
            else if (oldState == SimulationState.Simulating) // Simulating -> Something
            {
                if (newState == SimulationState.Menu) // Simulating -> Menu
                {
                    // Propagate a message to all listeners to notify them
                    Message message = new Message((int)MessageDestination.SIMULATION_END, "", null);
                    Controller.PropagateMessage(message);
                }
                else if (newState == SimulationState.Paused) // Simulating -> Paused
                {
                    // Propagate a message to all listeners to notify them
                    Message message = new Message((int)MessageDestination.SIMULATION_PAUSE, "", null);
                    Controller.PropagateMessage(message);
                }
            }
            else if (oldState == SimulationState.Paused) // Paused -> Something
            {
                if (newState == SimulationState.Menu) // Paused -> Menu
                {
                    // Propagate a message to all listeners to notify them
                    Message message = new Message((int)MessageDestination.SIMULATION_END, "", null);
                    Controller.PropagateMessage(message);
                }
                else if (newState == SimulationState.Simulating) // Paused -> Simulating
                {
                    // Propagate a message to all listeners to notify them
                    Message message = new Message((int)MessageDestination.SIMULATION_RESUME, "", null);
                    Controller.PropagateMessage(message);
                }
            }


            //    // Loading finished
            //    if (oldState == SimulationState.Loading)
            //    {
            //        // Propagate a message to all listeners to notify them that the simulation has loaded.
            //        Message message = new Message((int)MessageDestination.SIMULATION_LOADING, "", null);
            //        Controller.PropagateMessage(message);
            //    }
            //    else
            //    {
            //        if (newState == SimulationState.Simulating) // Started OR Resume
            //        {
            //            if(oldState == SimulationState.Menu) // Menu -> Simulation (Started simulation)
            //            {
            //                // Propagate a message to all listeners to notify them that the simulation has started.
            //                Message message = new Message((int)MessageDestination.SIMULATION_START, "", null);
            //                Controller.PropagateMessage(message);
            //            }
            //            else if(oldState == SimulationState.Simulating) // Paused -> Simulating ( Resume simulation)
            //            {
            //                // Propagate a message to all listeners to notify them that the simulation has started.
            //                Message message = new Message((int)MessageDestination.SIMULATION_RESUME, "", null);
            //                Controller.PropagateMessage(message);
            //            }
            //        }
            //        else if (oldState == SimulationState.Simulating) // Paused -> Simulating ( Resume simulation)
            //        {
            //            // Propagate a message to all listeners to notify them that the simulation has started.
            //            Message message = new Message((int)MessageDestination.SIMULATION_RESUME, "", null);
            //            Controller.PropagateMessage(message);
            //        }
            //        else if(oldState == SimulationState.Simulating && newState == SimulationState.Paused) // Simulation -> Paused (Paused simulation)
            //        {
            //            // Propagate a message to all listeners to notify them that the simulation has started.
            //            Message message = new Message((int)MessageDestination.SIMULATION_PAUSE, "", null);
            //            Controller.PropagateMessage(message);
            //        }
            //        else if (oldState == SimulationState.Simulating && newState == SimulationState.Menu) // Simulation -> Menu (Finished simulation)
            //        {
            //            // Propagate a message to all listners to notify them that the simulation has ended.
            //            Message message = new Message((int)MessageDestination.SIMULATION_END, "", null);
            //            Controller.PropagateMessage(message);
            //        }
            //    }
        }

    }
}