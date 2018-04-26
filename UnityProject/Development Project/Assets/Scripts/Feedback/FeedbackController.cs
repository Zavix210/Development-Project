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
            case (int)MessageDestination.DECISION_CHOICE_MADE: // A choice was made
                {
                    Decision choice = (Decision)message.Data;

                    // Fetch the time from the time controller
                    TimeController timeController = Controller.GetSimulationComponent<TimeController>();
                    float timestamp = timeController.GetCurrentTime();
                    float remainder = timeController.GetRemainingTime();

                    // Log the entry into the feedback
                    feedback.LogEntry(choice.Result, choice.DisplayText, choice.Feedback, timestamp, remainder);
                }
                break;
            case (int)MessageDestination.SIMULATION_END: // The simulation ended
                {
                    // Get a unique file name
                    string fileName = GetFileName();

                    StreamWriter writer = new StreamWriter(fileName, false);
                    feedback.WriteFeedbackToFile(writer);
                    writer.Close();

                    // Clear the feedback for the simulation
                    feedback.Clear();
                }
                break;
        }
    }

    /// <summary>
    /// Get the next available file.
    /// </summary>
    /// <returns></returns>
    private string GetFileName()
    {
        // TODO:
        // There is a possible race condition because the file exist check could have a file created inbetween.

        string prefix = "Log_";
        string format = ".txt";
        int i = 0;

        // Find a file name which is valid by iterating to find an available name with an index
        while(true)
        {
            i++;
            string fStr = prefix + i.ToString() + format;
            if(!File.Exists(fStr))
            {
                return fStr;
            }
        }
    }
}