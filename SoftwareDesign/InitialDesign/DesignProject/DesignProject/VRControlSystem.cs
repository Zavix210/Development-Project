using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignProject
{
    public class VRControlSystemEventArgs : EventArgs
    {

    }

    public class VRControlSystem : SimulationComponentBase
    {
        public EventHandler<VRControlSystemEventArgs> eventHandler;

        public VRControlSystem(SimulationController controller) : base(controller)
        {

        }
    }
}