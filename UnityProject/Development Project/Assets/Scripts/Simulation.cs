using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimulationSystem;

public class Simulation : MonoBehaviour
{
    private SimulationController controller;

    void Awake()
    {
        controller = new SimulationController();

        // Add some core components.
        controller.AddSimulationComponent<DecisionMaker>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
