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

        public abstract void OnInitialize();
    }
}
