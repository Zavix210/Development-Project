using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class DecisionTimelineAction : TimelineActionBase
{
    private DecisionSet decisionSet;

    public override void ExecuteAction()
    {
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;

        //Debug.Log("DECISION");

        // Get the decision controller and activate the decision set
        DecisionController decisionController = controller.GetSimulationComponent<DecisionController>();
        decisionController.StartDecisionProcess(decisionSet);
    }

    public void SetDecisionSet(DecisionSet set)
    {
        decisionSet = set;

        // Set the action time to be the decision sets specified time
        SetTimeOfAction(set.Time);
    }
}
