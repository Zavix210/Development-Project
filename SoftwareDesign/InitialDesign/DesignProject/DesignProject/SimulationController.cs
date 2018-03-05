using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignProject
{
    public class SimulationController
    {
        // TODO: Consider storing all SimulationComponentBase objects in a list (more maintainable)

        private VideoPlayer videoPlayer;
        private VideoStore videoStore;
        private DecisionMaker decisionMaker;
        private VRControlSystem vrControlSystem;
        private UIController uiController;
        private ParticleController particleController;

        public void Load(string fileName)
        {
            //...
        }
    }
}