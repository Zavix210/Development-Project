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

    public SimulationController Controller { get { return controller; } }
    public static Simulation Instance { get { return instance; } }

    void Awake()
    {
        // TODO: Implement a safer singleton
        instance = this;

        // Create the simulation controller instance
        controller = new SimulationController();

        // Add some core components.
        controller.AddSimulationComponent<DecisionMaker>();
        controller.AddSimulationComponent<VideoController>();
        controller.AddSimulationComponent<UIController>();

        // Initialize the simulation.
        controller.Initialize();
    }

	// Use this for initialization
	void Start ()
    {

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
    }
}
