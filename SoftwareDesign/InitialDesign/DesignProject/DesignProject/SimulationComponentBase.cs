using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignProject
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