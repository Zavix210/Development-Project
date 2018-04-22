using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;
using System.IO;

public class FeedbackController : SimulationComponentBase
{
    private Feedback feedback;

    public FeedbackController(SimulationController controller) : base(controller)
    {
        feedback = new Feedback();
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {
        switch(message.Route)
        {
            case (int)MessageDestination.DECISION_CHOICE_MADE:
                {
                    Decision choice = (Decision)message.Data;

                    // Fetch the time from the time controller
                    TimeController timeController = Controller.GetSimulationComponent<TimeController>();
                    float timestamp = timeController.GetCurrentTime();

                    // Log the entry into the feedback
                    feedback.LogEntry(choice.Result, choice.DisplayText, choice.Feedback, timestamp);
                }
                break;
            case (int)MessageDestination.SIMULATION_END:
                {
                    StreamWriter writer = new StreamWriter("Log.txt");
                    feedback.WriteFeedbackToFile(writer);
                    writer.Close();
                }
                break;
        }
    }
}