using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

enum DecisionControllerState
{
    Inactive,           // Not active
    WaitingForChoice,   // Waiting for a UI choice to be picked
    DisplayingResult,   // Displaying some results on-screen
}

enum DecisionControllerMode
{
    Standard,           // Does not repeat decisions
    RepeatUtilCorrect,  // Repeats decisions until the correct choice is made
}

public class DecisionController : SimulationComponentBase, IUnityHook
{
    private DecisionSet activeDecisionSet;
    private Decision choice;
    private DecisionControllerState state;
    private DecisionControllerMode mode;
    private float decisionWaitTime;
    private float time;

    // Colours
    private Color defaultColour;
    private Color incorrectColour;
    private Color correctColour;

    public DecisionController(SimulationController controller) : base(controller)
    {
        Simulation sim = Simulation.Instance;
        sim.AddHook(this);

        decisionWaitTime = 2.0f;
        mode = DecisionControllerMode.RepeatUtilCorrect;

        defaultColour = Color.white + new Color(0,0,0,1);
        correctColour = Color.green - Color.grey + new Color(0, 0, 0, 1);
        incorrectColour = Color.red - Color.grey + new Color(0, 0, 0, 1);

        ResetState();
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public void StartDecisionProcess(DecisionSet decisionSet)
    {
        if (decisionSet != null)
        {
            OnDecisionProcessStart(decisionSet);
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
                            OnChoiceMade(decision);
                        }
                        else // Cannot find the chosen decision
                        {
                            Debug.LogWarning("Failed to get decision with ID " + choice);
                            ResetState();
                            HideChoices();
                            HideFeedback();
                        }
                    }
                    else // Invalid, reset state
                    {
                        ResetState();
                        HideChoices();
                        HideFeedback();
                    }
                }
                break;
            case (int)MessageDestination.SIMULATION_END: // Simulation ending
                {
                    // Is there a decision active
                    if(state != DecisionControllerState.Inactive)
                    {
                        ResetState();
                        HideChoices();
                        HideFeedback();
                    }
                }
                break;
        }
    }

    private void ResetState()
    {
        state = DecisionControllerState.Inactive;
    }

    private void ShowChoices()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();

        // Clear all the active buttons (if any)
        uiController.ClearActiveButtons();

        // Place all the buttons containing the information about the decisions
        List<Decision> decisions = activeDecisionSet.GetDecisions();
        foreach (Decision decision in decisions)
        {
            uiController.PlaceButton(decision.Identifier, decision.DisplayText);
        }
    }

    private void HideChoices()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();

        // Clear all the active buttons (if any)
        uiController.ClearActiveButtons();
    }

    private void ShowFeedback()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();

        uiController.SetCentralDisplayTextVisibility(true);
        uiController.SetCentralDisplayText(choice.Feedback);

        if(choice.Result == DecisionResult.Correct)
        {
            uiController.SetCentralDisplayColour(correctColour);
        }
        else
        {
            uiController.SetCentralDisplayColour(incorrectColour);
        }
    }

    private void HideFeedback()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();
        uiController.SetCentralDisplayTextVisibility(false);
        uiController.SetCentralDisplayText("");
        uiController.SetCentralDisplayColour(defaultColour);
    }

    private void OnChoiceMade(Decision decision)
    {
        choice = decision;
        state = DecisionControllerState.DisplayingResult;
        time = 0.0f;

        // Hide the choices
        HideChoices();

        // Show the feedback
        ShowFeedback();
    }

    private void OnDecisionProcessStart(DecisionSet decisionSet)
    {
        Debug.Log("Decision Process started");

        activeDecisionSet = decisionSet;

        // Start the decision process
        DecisionProcess();

        // Pause the simulation
        Controller.PauseSimulation();

        // Post a message to notify all other components that the decision process has started
        Message message = new Message((int)MessageDestination.DECISION_START, "", activeDecisionSet);
        Controller.PropagateMessage(message);
    }

    private void OnDecisionProcessFinish()
    {
        Debug.Log("Decision Process ended");

        // Reset to the original state
        ResetState();

        // Hide the feedback UI
        HideFeedback();

        // Resume the simulation
        Controller.ResumeSimulation();

        // Post a message to notify all other components that the decision process has ended
        Message resultMessage = new Message((int)MessageDestination.DECISION_END, "", choice);
        Controller.PropagateMessage(resultMessage);
    }

    private void OnFeedbackComplete()
    {
        switch (mode)
        {
            case DecisionControllerMode.Standard:
                OnDecisionProcessFinish();
                break;
            case DecisionControllerMode.RepeatUtilCorrect:
                if (choice.Result == DecisionResult.Correct)
                {
                    OnDecisionProcessFinish();
                }
                else // Repeat when incorrect
                {
                    HideFeedback();
                    DecisionProcess();
                }
                break;
        }
    }

    private void DecisionProcess()
    {
        state = DecisionControllerState.WaitingForChoice;

        // Place the choices
        ShowChoices();
    }

    private void StartDisplay()
    {
        state = DecisionControllerState.DisplayingResult;
    }

    private void HideChoicesO()
    {
        ResetState();

        // Clean the UI
        //CleanupChoices();

        // Resume the simulation
        Controller.ResumeSimulation();

        // Post a message to notify all other components that the decision process has ended
        Message resultMessage = new Message((int)MessageDestination.DECISION_END, "", choice);
        Controller.PropagateMessage(resultMessage);
    }









    private void DecisionStarted()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();

        // Clear all the active buttons (if any)
        uiController.ClearActiveButtons();

        // Place all the buttons containing the information about the decisions
        List<Decision> decisions = activeDecisionSet.GetDecisions();
        foreach (Decision decision in decisions)
        {
            uiController.PlaceButton(decision.Identifier, decision.DisplayText);
        }

        // Pause the simulation until the decision has finished
        Controller.PauseSimulation();

        // Post a message to notify all other components that the decision process has started
        Message message = new Message((int)MessageDestination.DECISION_START, "", activeDecisionSet);
        Controller.PropagateMessage(message);
    }

    private void DecisionFinished(Decision decision)
    {
        // Post a message to notify all other components that the decision process has ended
        Message resultMessage = new Message((int)MessageDestination.DECISION_END, "", decision);
        Controller.PropagateMessage(resultMessage);

        // Cleanup visuals from the decision
        CleanupDecisionVisuals();
    }

    private void CleanupDecisionVisuals()
    {
        // Get the UI Controller
        UIController uiController = Controller.GetSimulationComponent<UIController>();

        // Clear all the active buttons
        uiController.ClearActiveButtons();

        // Resume the simulation once the decision has been made
        Controller.ResumeSimulation();

        // Clear the active decision set
        activeDecisionSet = null;
    }

    public void Update(float deltaTime)
    {
        if(state == DecisionControllerState.DisplayingResult)
        {
            time += deltaTime;

            // Has enough time passed to stop the result display?
            if(time >= decisionWaitTime)
            {
                OnFeedbackComplete();
            }
        }
    }
}