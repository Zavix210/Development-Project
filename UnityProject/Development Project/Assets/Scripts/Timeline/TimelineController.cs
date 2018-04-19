using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;

public class TimelineController : SimulationComponentBase, IUnityHook
{
    private Timeline timeline;

    public TimelineController(SimulationController controller) : base(controller)
    {
        timeline = new Timeline();

        // Self register and hook into the unity messages
        Simulation simulation = GameObject.FindObjectOfType<Simulation>();
        simulation.AddHook(this);
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