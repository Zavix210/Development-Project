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
        
        if(decisionSet != null)
        {
            UIController uiController = controller.GetSimulationComponent<UIController>();

            // Clear all the active buttons (if any)
            uiController.ClearActiveButtons();

            // Place all the buttons containing the information about the decisions
            List<Decision> decisions = decisionSet.GetDecisions();
            foreach(Decision decision in decisions)
            {
                uiController.PlaceButton(decision.Identifier, decision.DisplayText);
            }
        }
        else
        {
            Debug.LogWarning("Decision set is NULL");
        }
    }

    public void SetDecisionSet(DecisionSet set)
    {
        decisionSet = set;

        // Set the action time to be the decision sets specified time
        SetTimeOfAction(set.Time);
    }
}
