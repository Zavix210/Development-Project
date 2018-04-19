using System;
using System.Collections.Generic;

namespace SimulationSystem
{
    public enum SimulationState
    {
        Loading,
        Menu,
        Simulating,
        Paused
    }

    public class SimulationController
    {
        private List<SimulationComponentBase> simulationComponents;
        private MessageHandler messageHandler;
        private bool initialized;

        public SimulationController()
        {
            simulationComponents = new List<SimulationComponentBase>();
            messageHandler = new MessageHandler();
            initialized = false;

            // Add the important simulation components
            AddSimulationComponent<StateController>();
        }

        /// <summary>
        /// Initialize the simulation system and init each component
        /// </summary>
        public void Initialize()
        {
            // Ensure that initialization has not already occurred
            if(!initialized)
            {
                initialized = true;

                // Iterate over each component and notify them of initialization
                foreach(SimulationComponentBase item in simulationComponents)
                {
                    InitializeComponent(item);
                }
            }

            // Push the state component to the menu state now that initialization has finished
            FinishedLoading();
        }

        /// <summary>
        /// Get the simulation component of the desired type or NULL if it doesn't exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSimulationComponent<T>() where T : SimulationComponentBase
        {
            // Get the search type and cache it
            Type searchType = typeof(T);
            T searchItem = null;

            // Iterate over each of the components to find the desired component
            foreach (SimulationComponentBase item in simulationComponents)
            {
                // Check for a matching type
                if (item.GetType() == searchType)
                {
                    searchItem = (T)item;
                    break;
                }
            }

            return searchItem;
        }

        /// <summary>
        /// Add a simulation component of the desired type to the controller. If an instance of
        /// the desired type is already instantiated, it will be returned instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddSimulationComponent<T>() where T : SimulationComponentBase
        {
            // Try to get the component before creating a new instance
            T item = GetSimulationComponent<T>();

            // If the component is null, create an instance of it and store it
            if(item == null)
            {
                // Create a new instance of the target and call the constructor which suits the target
                // constructor of the component base object
                item = (T)Activator.CreateInstance(typeof(T), this);
                simulationComponents.Add(item);

                // Try to initialize the component
                InitializeComponent(item);
            }

            return item;
        }

        /// <summary>
        /// Propagate a message to all simulation components.
        /// </summary>
        /// <param name="message"></param>
        public void PropagateMessage(Message message)
        {
            messageHandler.ProcessMessage(message);
        }

        /// <summary>
        /// Initialize a component
        /// </summary>
        /// <param name="component"></param>
        private void InitializeComponent(SimulationComponentBase component)
        {
            // If the initialization step has already completed, auto-initialize the component upon creation
            if (initialized)
            {
                // Auto-register the component to receive messages
                messageHandler.AddReceiver(component);
                component.Initialize();
            }
        }

        /// <summary>
        /// Start the simulation and return whether the state of the simulation was actually changed.
        /// </summary>
        /// <returns></returns>
        public bool StartSimulation()
        {
            StateController stateController = GetSimulationComponent<StateController>();

            // Ensure the current state isn't already simulating.
            if (stateController.CurrentState != SimulationState.Simulating)
            {
                stateController.SetState(SimulationState.Simulating);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Stop the simulation and return whether the state of the simulation was actually changed.
        /// </summary>
        /// <returns></returns>
        public bool StopSimulation()
        {
            StateController stateController = GetSimulationComponent<StateController>();

            // Ensure the current state isn't already menu.
            if (stateController.CurrentState != SimulationState.Menu)
            {
                stateController.SetState(SimulationState.Menu);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Pause the simulation and return whether the state of the simulation was actually changed.
        /// </summary>
        /// <returns></returns>
        public bool PauseSimulation()
        {
            StateController stateController = GetSimulationComponent<StateController>();

            // Ensure the current state is simulating
            if (stateController.CurrentState == SimulationState.Simulating)
            {
                stateController.SetState(SimulationState.Paused);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Resume the simulation and return whether the state of the simulation was actually changed.
        /// </summary>
        /// <returns></returns>
        public bool ResumeSimulation()
        {
            StateController stateController = GetSimulationComponent<StateController>();

            // Ensure the current state is paused
            if (stateController.CurrentState == SimulationState.Paused)
            {
                stateController.SetState(SimulationState.Simulating);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ToggleSimulationPause()
        {
            StateController stateController = GetSimulationComponent<StateController>();

            if(stateController.CurrentState == SimulationState.Simulating) // Simulating -> Paused
            {
                return PauseSimulation();
            }
            else if(stateController.CurrentState == SimulationState.Paused) // Paused -> Simulating
            {
                return ResumeSimulation();
            }
            else // Not currently either state
            {
                return false;
            }
        }

        /// <summary>
        /// Notify the state controller to move to the menu state because loading has finished.
        /// </summary>
        private void FinishedLoading()
        {
            StateController stateController = GetSimulationComponent<StateController>();
            stateController.SetState(SimulationState.Menu);
        }
    }
}
