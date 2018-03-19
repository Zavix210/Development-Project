namespace SimulationSystem
{
    public abstract class SimulationComponentBase
    {
        private SimulationController controller;
        private bool initialized;

        public SimulationController Controller { get { return controller; } }

        public SimulationComponentBase(SimulationController controller)
        {
            this.controller = controller;
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
        /// Called when the controller has initialized, at this point it can be assumed that all other components 
        /// have been instantiated and can be accessed.
        /// </summary>
        public abstract void OnInitialize();
    }
}
