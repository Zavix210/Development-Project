using System;

namespace SimulationSystem
{
    /// <summary>
    /// A simulation component is a part of the simulation, some abstract methods are provided to
    /// ensure that components can interact with the simulation on a basic level.
    /// </summary>
    public abstract class SimulationComponentBase : IMessageReceiver
    {
        private SimulationController controller;
        private bool initialized;

        public SimulationController Controller { get { return controller; } }

        public SimulationComponentBase(SimulationController controller)
        {
            this.controller = controller;

            initialized = false;
        }

        /// <summary>
        /// Initialize the component and notify any derived component part.
        /// </summary>
        public void Initialize()
        {
            if(!initialized)
            {
                initialized = true;
                OnInitialize();
            }
        }

        /// <summary>
        /// Receive a message from an outside source, this should only be called if the message is
        /// on the route which this component accepts.
        /// </summary>
        /// <param name="message"></param>
        public void ReceiveMessage(Message message)
        {
            OnReceivedMessage(message);
        }

        /// <summary>
        /// Determine whether the message route is valid.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public bool IsRouteValid(int route)
        {
            return IsMessageRouteValid(route);
        }

        /// <summary>
        /// Called when the controller has initialized, at this point it can be assumed that all other components 
        /// have been instantiated and can be accessed.
        /// </summary>
        public abstract void OnInitialize();

        /// <summary>
        /// Called when a message has been received.
        /// </summary>
        public abstract void OnReceivedMessage(Message message);

        /// <summary>
        /// Called when a message is being received to determine whether it's valid for this component or not.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsMessageRouteValid(int route);
    }
}
