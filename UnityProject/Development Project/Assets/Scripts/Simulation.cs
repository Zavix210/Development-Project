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

    public SimulationController Controller { get { return controller; } }

    void Awake()
    {
        // Create the simulation controller instance
        controller = new SimulationController();

        // Add some core components.
        controller.AddSimulationComponent<DecisionMaker>();

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
		
	}
}
