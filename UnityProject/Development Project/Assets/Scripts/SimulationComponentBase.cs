namespace SimulationSystem
{
    public abstract class SimulationComponentBase
    {
        private SimulationController controller;

        public SimulationController Controller { get { return controller; } }

        public SimulationComponentBase(SimulationController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Called when the controller has initialized, at this point it can be assumed that all other components 
        /// have been instantiated and can be accessed.
        /// </summary>
        public abstract void OnInitialize();
    }
}
