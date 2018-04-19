using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimulationSystem;
using UnityEngine.Video;

/// <summary>
/// A class which acts as an interface between the Unity Engine and the simulation component system which
/// sits ontop of Unity but never directly relying on any of the functionality.
/// </summary>
public class Simulation : MonoBehaviour
{
    private SimulationController controller;
    private static Simulation instance;

    private List<IUnityHook> hooks;

    public SimulationController Controller { get { return controller; } }
    public static Simulation Instance { get { return instance; } }

    public bool AddHook(IUnityHook hook)
    {
        if(!hooks.Contains(hook))
        {
            hooks.Add(hook);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveHook(IUnityHook hook)
    {
        return hooks.Remove(hook);
    }

    void Awake()
    {
        hooks = new List<IUnityHook>();
    }

	// Use this for initialization
	void Start ()
    {
        // TODO: Implement a safer singleton
        instance = this;

        // Create the simulation controller instance
        controller = new SimulationController();

        // Add some core components.
        controller.AddSimulationComponent<SimulationSceneController>();
        controller.AddSimulationComponent<VideoController>();
        controller.AddSimulationComponent<UIController>();
        controller.AddSimulationComponent<TimeController>();
        controller.AddSimulationComponent<TimelineController>();

        // Initialize the simulation.
        controller.Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.F))
        {
            controller.StartSimulation();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            controller.StopSimulation();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            controller.ToggleSimulationPause();
        }

        float delta = Time.deltaTime;
        foreach(IUnityHook hook in hooks)
        {
            hook.Update(delta);
        }
    }
}
