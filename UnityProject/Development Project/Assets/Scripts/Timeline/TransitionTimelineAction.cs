using SimulationSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionTimelineAction : TimelineActionBase
{
    private int routeID;

    public TransitionTimelineAction(int routeID)
    {
        this.routeID = routeID;
    }

    public override void ExecuteAction()
    {
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;

        SimulationSceneController sceneController = controller.GetSimulationComponent<SimulationSceneController>();
        sceneController.ChangeScene(routeID);
    }
}