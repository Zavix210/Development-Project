using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class TimelineController : SimulationComponentBase, IUnityHook, ITimelineListener
{
    private Timeline timeline;

    public TimelineController(SimulationController controller) : base(controller)
    {
        timeline = new Timeline();
        timeline.AddListener(this);

        // Self register and hook into the unity messages
        Simulation simulation = GameObject.FindObjectOfType<Simulation>();
        simulation.AddHook(this);
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    /// <summary>
    /// Called when the timeline finishes.
    /// </summary>
    /// <param name="timeline"></param>
    public void OnTimelineFinished(Timeline timeline)
    {
        // Post a message to notify the timeline finishing
        Message message = new Message((int)MessageDestination.TIMELINE_FINISH, "", null);
        Controller.PropagateMessage(message);
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {
        switch (message.Route)
        {
            case (int)MessageDestination.SIMULATION_PAUSE:
                {
                    // Pause the timeline
                    timeline.SetPauseState(true);
                }
                break;
            case (int)MessageDestination.SIMULATION_RESUME:
                {
                    // Pause the timeline
                    timeline.SetPauseState(false);
                }
                break;
            case (int)MessageDestination.SIMULATION_START:
                {
                    //timeline.Reset();
                }
                break;
            case (int)MessageDestination.SIMULATION_END:
                {
                    timeline.ResetTimeline();
                }
                break;
            case (int)MessageDestination.SCENE_CHANGE: // A Scene change has occurred
                {
                    if (message.Identifier == "VALID")
                    {
                        SimulationScene scene = (SimulationScene)message.Data;

                        // Get the timeline actions and push them to the timeline
                        List<ITimelineAction> actions = timeline.GetRawActions();
                        scene.GetActions(actions);

                        string durationStr;
                        if (scene.GetAttribute("DURATION", out durationStr))
                        {
                            float duration;
                            if (float.TryParse(durationStr, out duration))
                            {
                                timeline.StartTimeline(duration);
                            }
                            else
                            {
                                Debug.LogWarning("Failed to parse scene duration");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Failed to get scene duration");
                        }
                    }
                }
                break;
        }
    }

    public void Update(float deltaTime)
    {
        // Query the timeline with the delta time
        timeline.QueryTimeline(deltaTime);
    }
}