namespace SimulationSystem
{
    public class SimulationComponentBase
    {
        private SimulationController controller;

        public SimulationController Controller { get { return controller; } }

        public SimulationComponentBase(SimulationController controller)
        {
            this.controller = controller;
        }
    }
}
