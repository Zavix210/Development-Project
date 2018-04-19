using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class DecisionController : SimulationComponentBase
{
    private DecisionSet activeDecisionSet;

    public DecisionController(SimulationController controller) : base(controller)
    {
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public void ActivateDecisionSet(DecisionSet decisionSet)
    {
        if (decisionSet != null)
        {
            activeDecisionSet = decisionSet;

            // Get the UI Controller
            UIController uiController = Controller.GetSimulationComponent<UIController>();

            // Clear all the active buttons (if any)
            uiController.ClearActiveButtons();

            // Place all the buttons containing the information about the decisions
            List<Decision> decisions = decisionSet.GetDecisions();
            foreach (Decision decision in decisions)
            {
                uiController.PlaceButton(decision.Identifier, decision.DisplayText);
            }

            // Pause the simulation until the decision has finished
            Controller.PauseSimulation();

            // Post a message to notify all other components that the decision process has started
            Message message = new Message((int)MessageDestination.DECISION_START, "", decisionSet);
            Controller.PropagateMessage(message);
        }
        else
        {
            Debug.LogWarning("Decision set is NULL");
        }
    }

    public override void OnReceivedMessage(Message message)
    {
        switch (message.Route)
        {
            case (int)MessageDestination.DECISION_UI_CHOICE: // A decision choice was made
                {
                    if (activeDecisionSet != null)
                    {
                        int choice = (int)message.Data;

                        Decision decision;
                        if (activeDecisionSet.GetDecisionWithID(choice, out decision))
                        {
                            // Get the UI Controller
                            UIController uiController = Controller.GetSimulationComponent<UIController>();

                            // Clear all the active buttons
                            uiController.ClearActiveButtons();

                            // Resume the simulation once the decision has been made
                            Controller.ResumeSimulation();

                            // Post a message to notify all other components that the decision process has ended
                            Message resultMessage = new Message((int)MessageDestination.DECISION_END, "", decision);
                            Controller.PropagateMessage(resultMessage);
                        }
                        else // Cannot find the chosen decision
                        {
                            Debug.LogWarning("Failed to get decision with ID " + choice);
                        }
                    }
                    
                }
                break;
        }
    }
}
