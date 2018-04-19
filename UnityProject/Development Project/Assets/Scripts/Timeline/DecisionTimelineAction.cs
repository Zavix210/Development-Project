using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class DecisionTimelineAction : TimelineActionBase
{
    public override void ExecuteAction()
    {
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;       
    }
}
