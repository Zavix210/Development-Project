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

    private Store<IUnityHook> hooks;

    public SimulationController Controller { get { return controller; } }
    public static Simulation Instance { get { return instance; } }

    public bool AddHook(IUnityHook hook)
    {
        return hooks.Add(hook);
    }

    public bool RemoveHook(IUnityHook hook)
    {
        return hooks.Remove(hook);
    }

    void Awake()
    {
        hooks = new Store<IUnityHook>();
    }

	// Use this for initialization
	void Start ()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple instances of singleton!");
        }

        // TODO: Implement a safer singleton
        instance = this;

        // Create the simulation controller instance
        controller = new SimulationController();

        // Add some core components.
        controller.AddSimulationComponent<SimulationSceneController>();
        controller.AddSimulationComponent<VideoController>();
        controller.AddSimulationComponent<UIController>();
        controller.AddSimulationComponent<TimelineController>();
        controller.AddSimulationComponent<DecisionController>();
        controller.AddSimulationComponent<FeedbackController>();

        TimeController timeController = controller.AddSimulationComponent<TimeController>();
        timeController.SetTimeLimit(60);

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
        //List<IUnityHook> hList = hooks.GetRawList();
        //for(int i = 0; i < hooks.Count; i++)
        //{
        //    IUnityHook hook = hooks[i];
        //    hook.Update(delta);
        //}

        List<IUnityHook> hList = hooks.GetRawList();
        foreach (IUnityHook hook in hList)
        {
            hook.Update(delta);
        }
    }
}
