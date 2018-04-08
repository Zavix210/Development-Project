using UnityEngine;
using System.Collections;

public enum MessageDestination
{
    DECISION_CHANGE = 37, // Change of decision
    SIMULATION_START = 1, // Simulation has started
    SIMULATION_END = 2, // Simulation has ended
}